using Microsoft.AspNet.Identity.EntityFramework;
using PCstore.Data.Model;
using PCstore.Data.Model.Contracts;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace PCstore.Data
{
    public class MsSqlDbContext : IdentityDbContext<User>
    {
        public MsSqlDbContext()
            : base("PCstore", throwIfV1Schema: false)
        {
        }

        public IDbSet<Computer> Computers { get; set; }
        public IDbSet<Laptop> Laptops { get; set; }
        public IDbSet<Display> Displays { get; set; }

        public override int SaveChanges()
        {            
            try
            {
                this.ApplyAuditInfoRules();
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join(Environment.NewLine, errorMessages);

                var exceptionMessage =
                    string.Concat($"{ex.Message}{Environment.NewLine}The validation errors are:{Environment.NewLine}{fullErrorMessage}");

                throw new DbEntityValidationException(exceptionMessage);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
        }

        private void ApplyAuditInfoRules()
        {
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditable)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        public static MsSqlDbContext Create()
        {
            return new MsSqlDbContext();
        }
    }
}
