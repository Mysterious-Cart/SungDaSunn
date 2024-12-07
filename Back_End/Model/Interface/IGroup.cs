using Back_End.Model.Crust_db;
using Back_End.Model.Interface;

public class IGroup{
    public ulong Id {get; set;}

    public ICollection<UserGroups>? Users { get; set; }

    public ICollection<Messages>? Messages {get; set;}
}