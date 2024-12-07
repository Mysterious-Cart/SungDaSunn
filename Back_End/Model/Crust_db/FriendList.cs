
namespace Back_End.Model.Crust_db;
public class FriendList{
    
    [GraphQLType(typeof(UnsignedLongType))]
    [GraphQLIgnore]
    public ulong Id {get; set;}

    [GraphQLType(typeof(UnsignedLongType))]
    [GraphQLIgnore]
    public ulong UserId {get; set;}

    [GraphQLDescription("If accessed from FriendsList, then this is the user; otherwise, it the Friend")]
    public User? User {get; set;}

    [GraphQLType(typeof(UnsignedLongType))]
    [GraphQLIgnore]
    public ulong FriendId {get; set;}

    [GraphQLDescription("If accessed from FriendedList, then this is the user; otherwise, it the Friend")]
    public User? Friend {get; set;}
    
}