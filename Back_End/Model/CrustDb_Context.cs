

namespace Model
{
    public class CrustDb_Context : Db
    {
        public CrustDb_Context(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}