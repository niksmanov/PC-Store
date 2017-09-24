using Microsoft.AspNet.Identity.EntityFramework;
using PCstore.Data.Models;

namespace PCstore.Data
{
    public class DbContext : IdentityDbContext<User>
    {
        public DbContext()
            : base("PCstore", throwIfV1Schema: false)
        {
        }

        public static DbContext Create()
        {
            return new DbContext();
        }
    }
}
