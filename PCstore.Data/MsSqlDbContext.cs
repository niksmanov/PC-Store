using Microsoft.AspNet.Identity.EntityFramework;
using PCstore.Data.Model;
using PCstore.Data.Model.Contracts;
using System;
using System.Data.Entity;
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
            this.ApplyAuditInfoRules();
            return base.SaveChanges();

            // FOR TESTING PURPOSES ONLY!!! \\
            //try
            //{
            //    return base.SaveChanges();
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    const string fileName = "EF_ERRORS.txt";
            //    var writer = new StreamWriter(fileName);
            //    string sourcePdfFilePath = $"{Directory.GetCurrentDirectory()}\\{fileName}";
            //    string destPdfFilePath = $"../../../{fileName}";

            //    // Retrieve the error messages as a list of strings.
            //    var errorMessages = ex.EntityValidationErrors
            //            .SelectMany(x => x.ValidationErrors)
            //            .Select(x => x.ErrorMessage);

            //    // Join the list to a single string.
            //    var fullErrorMessage = string.Join(Environment.NewLine, errorMessages);

            //    // Combine the original exception message with the new one.
            //    var exceptionMessage =
            //        string.Concat($"{ex.Message} {Environment.NewLine}The validation errors are: {Environment.NewLine}{fullErrorMessage}");

            //    writer.Write(exceptionMessage);
            //    writer.Close();
            //    File.Move(sourcePdfFilePath, destPdfFilePath);
            //    throw new DbEntityValidationException($"Check your errors on C:\\{fileName}");
            //}
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
