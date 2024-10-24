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
    }
}
