using Back_End.Model.Crust_db;
using Microsoft.AspNetCore.Components;

namespace Back_End.Request_Handler
{
    public class Query
    {
        [GraphQLDescription("Get all user. avoid calling this query if not necessary. Get the result from Login(Mutation) instead.")]
        public async Task<IQueryable<User>> GetUser([Service] Crust_Service service)
        {
            return await service.GetUser();
        }
        
        [GraphQLDescription("Get message of a group")]
        public async Task<IEnumerable<Messages>> GetMessages([Service] Crust_Service service,ulong SessionToken ,ulong GroupId){
            return await service.AuthenticateSession(SessionToken)?await service.GetMessagesFrom(GroupId):
                throw new GraphQLException(
                            ErrorBuilder
                            .New()
                            .SetMessage("Session token doesn't exist")
                            .SetCode("NAR")
                            .Build());
        }
        
        [GraphQLDeprecated("It not necessary. Try accessing it through GetUser.friend instead")]
        public async Task<IEnumerable<User?>> GetFriendOfUser([Service] Crust_Service service,[ID] ulong UserId){
            return await service.GetFriendOfUser(UserId);
        }
        
        [GraphQLDescription("Get the UserId of the closest match to the given Username.")]
        public async Task<User> GetUserByName([Service] Crust_Service service, string name){
            return await service.GetUser((await service.GetUserIdFromName(name)).GetValueOrDefault())??
                throw new GraphQLException(
                            ErrorBuilder
                            .New()
                            .SetMessage("Can't find user by this name.")
                            .SetCode("NAR")
                            .Build());
        }

        [GraphQLDeprecated("It not necessary. UserGroup is accessible through GetUser")]
        [GraphQLDescription("Get the GroupId that match the users specify in the array. (Return a perfect match, no more or no less user)")]
        public async Task<Group> GetGroupWithThisUser([Service] Crust_Service service, ulong[] users){
            return await service.GetGroupById(await service.GetGroupId(users));
        }
        
    }
}
