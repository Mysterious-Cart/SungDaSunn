using Back_End.Model.Crust_db;
using Back_End.Model.Interface;

public class Message_Result : IReturn 
{
    public bool Sent {get; set;}
    public DateTime Sent_Time {get; set;}
}
