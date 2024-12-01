using Back_End.Model.Crust_db;

public class LoginToken{
    public ulong SessionId {get; set;}
    public ulong UserId {get; set;}
    public User? user {get; set;}
    public DateTime Last_Login {get; set;}
    
}