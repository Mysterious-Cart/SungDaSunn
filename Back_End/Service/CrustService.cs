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
    public async Task<IQueryable<User?>> GetFriendOfUser(ulong userId){
        var item = context.User.Include("Friend.Friend").SelectMany(i => i.Friend!.Select(i => i.Friend));
        return await Task.FromResult(item);
    }
    public async Task<IQueryable<Messages>> GetMessagesFrom(ulong GroupId){
        var item = context.Groups
                        .Include(i => i.Messages)
                        .First(i => i.Id  == GroupId)
                        .Messages
                        .AsQueryable();

        return await Task.FromResult(item);
    }
    public async Task<bool> sentMessage(ulong GroupId, ulong SenderId, string Messages, DateTime SentTime ){
        Messages newMessages = new Messages{
            Id = generateId(),
            GroupId = GroupId,
            SenderId = SenderId,
            Message = Messages,
            DateTime = SentTime,
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
    public async Task<bool> createUser(string name, string password, string email = ""){
        User newUser = new User{
            Id = generateId(),
            Name = name,
            Password = password,
            Email = email
        };

        try{
            await context.User.AddAsync(newUser);
            await context.SaveChangesAsync();
        }catch{
            return false;
        }
        return true;
    }
 
    private ulong generateId() => (ulong)Snippy.LongRandom(0, 100000000000000000, new Random());


}