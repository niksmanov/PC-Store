using Microsoft.AspNet.Identity.EntityFramework;
using PCstore.Data.Models;
using System.Data.Entity;

namespace PCstore.Data
{
    public class DbContext : IdentityDbContext<User>
    {
        public DbContext()
            : base("PCstore", throwIfV1Schema: false)
        {
        }


        public IDbSet<Computer> Computers { get; set; }
        public IDbSet<Laptop> Laptops { get; set; }
        public IDbSet<Display> Displays { get; set; }


        public static DbContext Create()
        {
            return new DbContext();
        }
    }
}
