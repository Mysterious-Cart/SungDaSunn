using Back_End.Model.Crust_db;
using Microsoft.AspNetCore.Components;

namespace Back_End.Request_Handler
{
    public class Query
    {
        public async Task<IEnumerable<User>> GetUser([Service] Crust_Service service)
        {
            return await service.GetUser();
        }
        public async Task<IEnumerable<Messages>> GetMessages([Service] Crust_Service service, [GraphQLType(typeof(UnsignedLongType))] ulong GroupId){
            return await service.GetMessagesFrom(GroupId);
        }
        public async Task<IEnumerable<User?>> GetFriendOfUser([Service] Crust_Service service, [GraphQLType(typeof(UnsignedLongType))] ulong UserId){
            return await service.GetFriendOfUser(UserId);
        }

        public async Task<User> GetUserByName([Service] Crust_Service service, string name){
            return await service.GetUser((await service.GetIdFromName(name)).GetValueOrDefault())??
                throw new GraphQLException(
                            ErrorBuilder
                            .New()
                            .SetMessage("Can't find user by this name.")
                            .SetCode("NAR")
                            .Build());
        }

        public async Task<int> GetGroupIdWithThisUser(
            [Service] Crust_Service service, 
            ulong UserId,
            ulong FriendId
        ){
            return await service.GetGroupId(UserId, FriendId);
        }

    }
}
