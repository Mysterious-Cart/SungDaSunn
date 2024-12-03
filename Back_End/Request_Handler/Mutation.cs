using System.Runtime.InteropServices;
using Back_End;
using Back_End.Model.Crust_db;
using HotChocolate.AspNetCore;
using HotChocolate.Subscriptions;

public class Mutation{

    GraphQLException SessionNoExistError = 
        new GraphQLException(ErrorBuilder
                .New()
                .SetMessage("Session doesn't exist")
                .SetCode("NAR")
                .Build());
    
    GraphQLException InternalError = 
        new GraphQLException(ErrorBuilder
                .New()
                .SetMessage("Internal error catched.")
                .SetCode("CRF")
                .Build());
    
    [GraphQLDescription("Send a message to the given groupId")]
    public async Task<Message_Result> Send_Message(
        [Service] Crust_Service _Service, 
        ulong SessionId,
        ulong GroupId, 
        ulong SenderId,
        string Messages
    ){
        return await _Service.AuthenticateSession(SessionId)? 
        new Message_Result{ Sent = await _Service.sentMessage(GroupId, SenderId,Messages, DateTime.Now) }:
            throw SessionNoExistError;
    }

    [GraphQLDescription("Register a new user.")]
    public async Task<User> Register_User(
        [Service] Crust_Service service, 
        string name, 
        string password, 
        [Optional] string email
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

        var resultedId = await service.createUser(name, password, email)??
            throw new GraphQLException(
                    ErrorBuilder
                    .New()
                    .SetMessage("Failed to create User")
                    .SetCode("CRF")
                    .Build());

        return await service.GetUser(resultedId);
    }

    [GraphQLDescription("Login using SessionId for quick login.")]
    public async Task<LoginToken> LoginViaSessionId([Service] Crust_Service service, ulong SessionToken){
        return await service.loginViaSessionId(SessionToken)??
        throw SessionNoExistError;
    }
    
    [GraphQLDescription("Normal login method. (Return Login Token which contain User Info inside.)")]
    public async Task<LoginToken> Login([Service] Crust_Service service, string Username, string Password){
        return await service.login(Username, Password)??
        throw new GraphQLException(
                ErrorBuilder
                .New()
                .SetMessage("Username or Password is wrong")
                .SetCode("NAR")
                .Build());
    }
    public async Task<ulong> Add_Friend([Service] Crust_Service service, ulong senderId, ulong RequestTo_Id){
        return await service.addFriend(senderId, RequestTo_Id)??throw InternalError;
    }

    public async Task<ulong> Confirm_FriendRequest([Service] Crust_Service service, ulong Requests_Id){
        return await service.ConfirmFriendRequest(Requests_Id)??throw InternalError;
    }

    
}

// CRF: Creation Failed.
// ALE: Already Exist.
// NAR: No Result.