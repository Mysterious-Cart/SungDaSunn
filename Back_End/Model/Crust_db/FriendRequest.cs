using System.Diagnostics.CodeAnalysis;

namespace Back_End.Model.Crust_db;
public class FriendRequest{

    public ulong Id {get; set;}
    public ulong SenderId {get; set;}
    public User? Sender {get; set;}
    public ulong RequestToId {get; set;}
    public User? RequestTo {get; set;}
}