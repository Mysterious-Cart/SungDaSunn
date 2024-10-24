using Microsoft.AspNetCore.Components;
using Back_End.Model;
using Back_End.Model.Crust_db;
using Microsoft.EntityFrameworkCore;

namespace Back_End;
public class Crust_Service
{
    CrustDb_Context Context
    {
        get
        {
            return this.context;
        }
    }

    private readonly CrustDb_Context context;
    public Crust_Service(CrustDb_Context context)
    {
        this.context = context;
    }

    public async Task<IQueryable<User>> GetUser()
    {
        var item = context.User.AsQueryable();
        item = item.Include(i => i.Groups);
        item = item.Include("Groups.GroupInfo");

        return await Task.FromResult(item);
    }

}