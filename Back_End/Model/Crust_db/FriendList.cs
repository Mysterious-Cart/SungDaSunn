
namespace Back_End.Model.Crust_db;
public class FriendList{
    
    public ulong Id {get; set;}
    public ulong UserId {get; set;}
    public User? User {get; set;}
    public ulong FriendId {get; set;}
    public User? Friend {get; set;}
    
}