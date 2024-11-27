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
        public async Task<IEnumerable<Messages>> GetMessages([Service] Crust_Service service, [GraphQLType("Int!")] ulong GroupId, DateTime From, DateTime To){
            return await service.GetMessagesFrom(GroupId);
        }
        public async Task<IEnumerable<User?>> FriendOfUser([Service] Crust_Service service, [GraphQLType("Int!")] ulong UserId){
            return await service.GetFriendOfUser(UserId);
        }

    }
}
