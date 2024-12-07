public interface IUser{
    public ulong Id {get; set;}
    public string Name {get; set;}

    [GraphQLType(typeof(EmailAddressType))]
    public string Email { get; set; }
}