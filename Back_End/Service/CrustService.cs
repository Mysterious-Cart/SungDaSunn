using Back_End.Model;
using Back_End.Model.Crust_db;
using Microsoft.EntityFrameworkCore;
using HotChocolate.Subscriptions;
using System.Net.Mime;

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
    public async Task<IQueryable<User>> GetUser(    )
    {
        var item = context.User
            .Include(i => i.Groups)
            .Include("Groups.GroupInfo");

        return await Task.FromResult(item);
    }
    public async Task<User> GetUser(ulong Id){
        var user = context.User.First(i => i.Id == Id);

        return await Task.FromResult(user);
    }
    public async Task<IQueryable<User?>> GetFriendOfUser(ulong userId){
        var item = context.User.Include("Friend.Friend").SelectMany(i => i.Friend!.Select(i => i.Friend));
        return await Task.FromResult(item);
    }
    public async Task<IQueryable<Messages>> GetMessagesFrom(ulong GroupId){
        var item = context.Groups
                        .Include(i => i.Messages)
                        .Include("Messages.Sender")
                        .First(i => i.Id == GroupId)
                        .Messages?
                        .AsQueryable();

        return await Task.FromResult(item)??Enumerable.Empty<Messages>().AsQueryable();
    }
    public async Task<bool> IsNameTaken(string Name){
        return await GetIdFromName(Name, Approximate.exact) == null?false:true;
    }
    public async Task<ulong?> GetIdFromName(string Name, Approximate approx = 0){
        var user = await context.User.FirstOrDefaultAsync(i => approx == Approximate.close?i.Name.Contains(Name):i.Name == Name);
        return user?.Id;
    }
    public async Task<int> GetGroupId(ulong userid, ulong friendId){
        var item = context.Groups.First(i => i.Users.Contains(i.Users.First(i => i.UserId == userid)) && i.Users.Contains(i.Users.First(i => i.UserId == friendId)));
        return (int)(await Task.FromResult(item)).Id;
    }
    public async Task<bool> sentMessage(ulong GroupId, ulong SenderId, string Messages, DateTime SentTime ){
        Messages newMessages = new Messages{
            Id = generateId(),
            GroupId = GroupId,
            SenderId = SenderId,
            Message = Messages,
            DateTime = SentTime,
            Sender = await GetUser(SenderId)??null,
        };
        try{
            await context.Messages.AddAsync(newMessages);
            await context.SaveChangesAsync();
        }catch{
            return false;
        }
        
        await sender.SendAsync(GroupId.ToString(), newMessages);
        return true;
    }
    public async Task<ulong?> createUser(string name, string password, string email = ""){
        var id = generateId();
        User newUser = new User{
            Id = id,
            Name = name,
            Password = password,
            Email = email
        };

        try{
            await context.User.AddAsync(newUser);
            await context.SaveChangesAsync();
        }catch{
            return null;
        }
        return id;
    }
    public async Task<ulong?> createGroup(ulong[] members){
        var groupid = generateId();
        Group newGroup = new Group{
            Id = groupid,
            Name = "",
        };

        await context.Groups.AddAsync(newGroup);
        
        try{
            await context.SaveChangesAsync();
        }catch{
            return null;
        }


        foreach(ulong i in members){
            UserGroups user = new UserGroups{
                UserId = i,
                GroupId = groupid,
            };
            await context.UserGroups.AddAsync(user);
        }
        try{
            await context.SaveChangesAsync();
        }catch{
            return null;
        }
        
        return groupid;
    }
    private ulong generateId() => (ulong)Snippy.LongRandom(0, 1000000000, new Random());


}

public enum Approximate
{
    close,
    exact,
}