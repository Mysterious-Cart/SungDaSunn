using Back_End;
using Back_End.Model.Crust_db;
using HotChocolate.AspNetCore;
using HotChocolate.Subscriptions;

public class Mutation{

    public async Task<bool> SendMessage(
        [Service] Crust_Service _Service, 
        ulong GroupId, 
        ulong SenderId,
        string Messages
    ){
        return await _Service.sentMessage(GroupId, SenderId,Messages, DateTime.Now);
    }
    public async Task<ulong> CreateUser(
        [Service] Crust_Service service, 
        string name, 
        string password, 
        string email = ""
    ){
        if(await service.IsNameTaken(name) == true){
            throw new GraphQLException(
                ErrorBuilder
                .New()
                .SetMessage("User already exist: " + (await service.GetIdFromName(name)).ToString()??"")
                .SetExtension("Id", (await service.GetIdFromName(name)).ToString()??"")
                .SetCode("ALE")
                .Build());
        }

        return await service.createUser(name, password, email = "")??
            throw new GraphQLException(
                    ErrorBuilder
                    .New()
                    .SetMessage("Failed to create User")
                    .SetCode("CRF")
                    .Build());
    }
    public async Task<ulong> CreateGroup([Service] Crust_Service service,ulong[] userIds ){
        return await service.createGroup(userIds)??
            throw new GraphQLException(
                            ErrorBuilder
                            .New()
                            .SetMessage("Failed to create Group")
                            .SetCode("CRF")
                            .Build());
    }
}

// CRF: Creation Failed.
// ALE: Already Exist.
// NAR: No Result.