using Back_End;
using Back_End.Model.Crust_db;
using HotChocolate.AspNetCore;
using HotChocolate.Subscriptions;

public class Mutation{

    [GraphQLDescription("Send a message to the given groupId")]
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
                .SetMessage("User already exist: " + (await service.GetUserIdFromName(name)).ToString()??"")
                .SetExtension("Id", (await service.GetUserIdFromName(name)).ToString()??"")
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
    public async Task<ulong> CreateGroup([Service] Crust_Service service, ulong[] userIds ){
        return await service.createGroup(userIds)??
            throw new GraphQLException(
                            ErrorBuilder
                            .New()
                            .SetMessage("Failed to create Group")
                            .SetCode("CRF")
                            .Build());
    }
    
    [GraphQLDescription("Login using SessionId for quick login. (Return User Info.)")]
    public async Task<User> LoginViaSessionId([Service] Crust_Service service,[ID] ulong SessionToken){
        return await service.loginViaSessionId(SessionToken)??
        throw new GraphQLException(
                ErrorBuilder
                .New()
                .SetMessage("Session doesn't exist")
                .SetCode("NAR")
                .Build());;
    }
    
    [GraphQLDescription("Normal login. (Return Login Token which contain User Info inside.)")]
    public async Task<LoginToken> Login([Service] Crust_Service service, string Username, string Password){
        return await service.login(Username, Password)??
        throw new GraphQLException(
                ErrorBuilder
                .New()
                .SetMessage("Username or Password is wrong")
                .SetCode("NAR")
                .Build());
    }
}

// CRF: Creation Failed.
// ALE: Already Exist.
// NAR: No Result.