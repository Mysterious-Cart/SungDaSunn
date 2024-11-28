
namespace Back_End.Model.Crust_db;
public class FriendList{
    
    [GraphQLType(typeof(UnsignedLongType))]
    public ulong Id {get; set;}

    [GraphQLType(typeof(UnsignedLongType))]
    public ulong UserId {get; set;}
    public User? User {get; set;}

    [GraphQLType(typeof(UnsignedLongType))]
    public ulong FriendId {get; set;}
    public User? Friend {get; set;}
    
}