using Back_End.Model;
using Back_End.Model.Crust_db;
using Microsoft.EntityFrameworkCore;
using HotChocolate.Subscriptions;
using System.Net.Mime;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Identity;
using Back_End.Migrations;

namespace Back_End;
public class Crust_Service
{
    CrustDb_Context Context
    {
        get
        {
            return this.context;
        }
    }

    private readonly CrustDb_Context context;
    private readonly ITopicEventSender sender;
    public Crust_Service(CrustDb_Context context, ITopicEventSender sender)
    {
        this.context = context;
        this.sender = sender;
    }    
    public async Task<IQueryable<User>> GetUser()
    {
        var item = context.User
            .Include(i => i.Groups)
            .Include(i => i.Friends_List)
            .Include(i => i.Friended_List)
            .Include(i => i.RequestsFrom_List)
            .Include(i => i.RequestsTo_List)
            .Include("Groups.GroupInfo")
            .Include("Friends_List.Friend")
            .Include("Friended_List.User");


        return await Task.FromResult(item);
    }
    public async Task<User> GetUser(ulong Id)
    {
        var user = (await GetUser()).First(i => i.Id == Id);

        return user;
    }
    public async Task<IQueryable<User?>> GetFriendOfUser(ulong userId)
    {
        var item = context.User.Include("Friends_List.Friend").SelectMany(i => i.Friends_List!.Select(i => i.Friend));
        return await Task.FromResult(item);
    }
    public async Task<IQueryable<Messages>> GetMessagesFrom(ulong GroupId)
    {
        var item = context.Groups
                        .Include(i => i.Messages)
                        .Include("Messages.Sender")
                        .First(i => i.Id == GroupId)
                        .Messages?
                        .AsQueryable();

        return await Task.FromResult(item) ?? Enumerable.Empty<Messages>().AsQueryable();
    }
    public async Task<bool> IsNameTaken(string Name)
    {
        return await GetUserIdFromName(Name, Approximate.exact) == null ? false : true;
    }
    public async Task<ulong?> GetUserIdFromName(string Name, Approximate approx = 0)
    {
        var user = await context.User.FirstOrDefaultAsync(i => approx == Approximate.close ? i.Name.Contains(Name) : i.Name == Name);
        return user?.Id;
    }
    public async Task<ulong> GetGroupId(ulong[] users)
    {
        var item = context.Groups.First(i => i.Users!.All(i => users.Any(v => v == i.UserId)));
        return (await Task.FromResult(item)).Id;
    }
    public async Task<Group> GetGroupById(ulong Id){
        return await context.Groups
            .Include(i => i.Users)
            .Include("Users.User")
            .Include(i => i.Messages)
            .FirstAsync(i => i.Id == Id);
    }
    public async Task<bool> sentMessage(ulong GroupId, ulong SenderId, string Messages, DateTime SentTime)
    {
        Messages newMessages = new Messages
        {
            Id = generateId(),
            GroupId = GroupId,
            SenderId = SenderId,
            Message = Messages,
            DateTime = SentTime,
            Sender = await GetUser(SenderId) ?? null,
        };
        try
        {
            await context.Messages.AddAsync(newMessages);
            await context.SaveChangesAsync();
        }
        catch
        {
            return false;
        }

        await sender.SendAsync(GroupId.ToString(), newMessages);
        return true;
    }
    public async Task<ulong?> createUser(string name, string password, string email = "")
    {
        var id = generateId();
        User newUser = new User
        {
            Id = id,
            Name = name,
            Password = password,
            Email = email
        };

        try
        {
            await context.User.AddAsync(newUser);
            await context.SaveChangesAsync();
        }
        catch
        {
            return null;
        }
        return id;
    }
    public async Task<ulong?> createGroup(ulong[] members){
        var groupid = generateId();
        Group newGroup = new Group
        {
            Id = groupid,
            Name = "",
        };

        await context.Groups.AddAsync(newGroup);

        try
        {
            await context.SaveChangesAsync();
        }
        catch
        {
            return null;
        }


        foreach (ulong i in members)
        {
            UserGroups user = new UserGroups
            {
                UserId = i,
                GroupId = groupid,
            };
            await context.UserGroups.AddAsync(user);
        }
        try
        {
            await context.SaveChangesAsync();
        }
        catch
        {
            return null;
        }

        return groupid;
    }
    public async Task<LoginToken?> login(string Username, string Password){
        if(await context.User.AnyAsync(i => i.Name == Username && i.Password == Password) is not false){
            var userId = await GetUserIdFromName(Username);
            LoginToken SessionToken;
            if(context.SessionToken.Any(i => i.UserId == userId) is false){
                LoginToken token = new LoginToken{
                    SessionId = generateId(),
                    UserId = (ulong)userId,
                    Last_Login = DateTime.Now
                };
                await context.SessionToken.AddAsync(token);
            }else{
                (await context.SessionToken.FirstAsync(i => i.UserId == userId)).Last_Login = DateTime.Now;
            }

            try{
                await context.SaveChangesAsync();
                SessionToken = await Context.SessionToken.FirstAsync(i => i.UserId == userId);
                SessionToken.user = await GetUser(SessionToken.UserId);
            }catch{
                return null;
            }
            
            return SessionToken;
        }else{
            return null;
        }
    }
    public async Task<LoginToken?> loginViaSessionId(ulong SessionId){
        if(await context.SessionToken.AnyAsync(i => i.SessionId == SessionId) is true){
            var Token = await context.SessionToken.Include(i => i.user).FirstAsync(i => i.SessionId == SessionId);
            Token.Last_Login = DateTime.Now;
            try{
                await context.SaveChangesAsync();
            }catch{
                return null;
            }
            return Token;
        }else{
            return null;
        }
    }
    public async Task<bool> AuthenticateSession(ulong SessionId){
        if(await context.SessionToken.AnyAsync(i => i.SessionId == SessionId)){
            return true;
        }else{
            return false;
        }
    }
    public async Task<ulong?> addFriend(ulong SenderId, ulong RequestToId)
    {
        var Id = generateId();
        FriendRequest newFriendRequest = new FriendRequest{
            Id = Id,
            SenderId = SenderId,
            RequestToId = RequestToId,
        };
        try{
            await context.FriendRequests.AddAsync(newFriendRequest);
            await context.SaveChangesAsync();
        }catch{
            return null;
        }

        return Id;
    }
    public async Task<ulong?> ConfirmFriendRequest(ulong RequestId){
        try{
            var theRequest = await context.FriendRequests.FirstAsync(i => i.Id == RequestId);
            var genId = generateId();
            FriendList friend = new FriendList{
                Id = genId,
                UserId = theRequest.SenderId,
                FriendId = theRequest.RequestToId
            };

            context.FriendLists.Add(friend);
            await createGroup([theRequest.SenderId, theRequest.RequestToId]);
            context.FriendRequests.Remove(theRequest);
            await context.SaveChangesAsync();
            return genId;
        }catch{
            return null;
        }
        
    }
    private ulong generateId() => (ulong)Snippy.LongRandom(0, 1000000000, new Random());


}

public enum Approximate
{
    close,
    exact,
}