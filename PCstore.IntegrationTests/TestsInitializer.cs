using NUnit.Framework;
using PCstore.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace PCstore.IntegrationTests
{
    [TestFixture]
    public class TestsInitializer
    {
        public static void AssemblyInit(TestContext context)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MsSqlDbContext, TestDbConfiguration>());
        }
    }

    public sealed class TestDbConfiguration : DbMigrationsConfiguration<MsSqlDbContext>
    {
        public TestDbConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}

