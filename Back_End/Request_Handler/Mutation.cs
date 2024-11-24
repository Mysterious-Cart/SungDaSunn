using Back_End;
using Back_End.Model.Crust_db;
using HotChocolate.Subscriptions;

public class Mutation{

    public async Task<bool> SendMessage(
        [Service] Crust_Service _Service, 
        [GraphQLType("Int!")]ulong GroupId, 
        [GraphQLType("Int!")]ulong SenderId,string Messages
    ){
        return await _Service.sentMessage(GroupId, SenderId,Messages, DateTime.Now);
    }
}