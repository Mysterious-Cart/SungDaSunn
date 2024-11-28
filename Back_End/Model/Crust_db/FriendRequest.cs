using System.Diagnostics.CodeAnalysis;

namespace Back_End.Model.Crust_db;
public class FriendRequest{
    [GraphQLType(typeof(UnsignedLongType))]
    public ulong Id {get; set;}

    [GraphQLType(typeof(UnsignedLongType))]
    public ulong SenderId {get; set;}
    public User? Sender {get; set;}

    [GraphQLType(typeof(UnsignedLongType))]
    public ulong RequestToId {get; set;}
    public User? RequestTo {get; set;}
}