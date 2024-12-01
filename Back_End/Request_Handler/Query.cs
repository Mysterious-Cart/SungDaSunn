using Back_End.Model.Crust_db;
using Microsoft.AspNetCore.Components;

namespace Back_End.Request_Handler
{
    public class Query
    {
        [GraphQLDescription("Get all user.")]
        public async Task<IEnumerable<User>> GetUser([Service] Crust_Service service)
        {
            return await service.GetUser();
        }
        public async Task<IEnumerable<Messages>> GetMessages([Service] Crust_Service service,[ID] ulong GroupId){
            return await service.GetMessagesFrom(GroupId);
        }
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

        [GraphQLDescription("Get the GroupId that match the users specify in the array. (Return a perfect match, no more or no less user)")]
        public async Task<Group> GetGroupWithThisUser([Service] Crust_Service service, ulong[] users){
            return await service.GetGroupById(await service.GetGroupId(users));
        }
        
    }
}
