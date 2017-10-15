using Ninject;
using NUnit.Framework;
using PCstore.Data;
using PCstore.Data.Model;
using PCstore.Data.Repositories;
using PCstore.Web.App_Start;
using System;
using System.Linq;

namespace PCstore.IntegrationTests.Data
{
    [TestFixture]
    public class EfRepositoryIntegrationTests
    {
        private static IKernel kernel;


        private static User dbUser = new User()
        {
            Id = Guid.NewGuid().ToString(),
            CreatedOn = DateTime.Now,
            ModifiedOn = DateTime.Now,
            Email = "debugger@abv.bg",
            UserName = "debugger@abv.bg"
        };

        private static Computer dbComputer = new Computer()
        {
            CPU = "Intel Core I7700",
            CpuSpeed = 3,
            RAM = 4,
            RamType = "DDR4",
            HDD = 1000,
            GPU = "Nvidia GeForce 7700",
            GpuMemory = 2,
            OpticalDevice = "LG DVD-RW class 3",
            OperatingSystem = "Windows 10",
            Price = 1000m,
            SellerPhone = "0884936445",
            Description = "Good computer for this price!",
            Seller = dbUser,
            CreatedOn = DateTime.Now,
            ModifiedOn = DateTime.Now
        };

        private static User secondDbUser = new User()
        {
            Id = Guid.NewGuid().ToString(),
            CreatedOn = DateTime.Now,
            ModifiedOn = DateTime.Now,
            Email = "compilator@abv.bg",
            UserName = "compilator@abv.bg"
        };

        private static Computer secondDbComputer = new Computer()
        {
            CPU = "Intel Core I7700",
            CpuSpeed = 3,
            RAM = 4,
            RamType = "DDR4",
            HDD = 1000,
            GPU = "Nvidia GeForce 7700",
            GpuMemory = 2,
            OpticalDevice = "LG DVD-RW class 3",
            OperatingSystem = "Windows 10",
            Price = 1000m,
            SellerPhone = "0884936445",
            Description = "Good computer for this price!",
            Seller = secondDbUser,
            CreatedOn = DateTime.Now,
            ModifiedOn = DateTime.Now
        };

        [OneTimeSetUp]
        public void TestInit()
        {
            kernel = InjectionConfig.CreateKernel();
            var dbContext = kernel.Get<MsSqlDbContext>();

            dbContext.Users.Add(dbUser);
            dbContext.SaveChanges();

            dbContext.Computers.Add(dbComputer);
            dbContext.SaveChanges();
        }

        [OneTimeTearDown]
        public void TestCleanup()
        {
            var dbContext = kernel.Get<MsSqlDbContext>();

            foreach (var computer in dbContext.Computers)
            {
                dbContext.Computers.Attach(computer);
                dbContext.Computers.Remove(computer);
            }

            foreach (var user in dbContext.Users)
            {
                dbContext.Users.Attach(user);
                dbContext.Users.Remove(user);
            }

            dbContext.SaveChanges();
        }

        [Test]
        public void EfRepository_ShouldAddComputer_IfValid()
        {
            // Arrange
            kernel = InjectionConfig.CreateKernel();
            var dbContext = kernel.Get<MsSqlDbContext>();

            var efRepository = kernel.Get<IEfRepository<Computer>>();
            var computersCount = dbContext.Set<Computer>().Count();


            // Act
            efRepository.Add(secondDbComputer);
            dbContext.SaveChanges();

            var computersCountAfterAdding = dbContext.Set<Computer>().Count();


            // Assert
            Assert.AreEqual(computersCount, computersCountAfterAdding);
        }

        [Test]
        public void EfRepository_ShouldDeleteComputer_IfValid()
        {
            // Arrange
            kernel = InjectionConfig.CreateKernel();
            var dbContext = kernel.Get<MsSqlDbContext>();

            var efRepository = kernel.Get<IEfRepository<Computer>>();
            var computersCount = dbContext.Set<Computer>().Count();


            // Act
            efRepository.Delete(secondDbComputer);
            dbContext.SaveChanges();

            var computersCountAfterDeleting = dbContext.Set<Computer>().Count();


            // Assert
            Assert.AreEqual(computersCount, computersCountAfterDeleting);
        }

        [Test]
        public void EfRepository_ShouldUpdateComputer_IfValid()
        {
            // Arrange
            kernel = InjectionConfig.CreateKernel();
            var dbContext = kernel.Get<MsSqlDbContext>();

            var efRepository = kernel.Get<IEfRepository<Computer>>();
            var computersCount = dbContext.Set<Computer>().Count();


            // Act
            efRepository.Update(secondDbComputer);
            dbContext.SaveChanges();

            // Assert
            Assert.IsInstanceOf<Computer>(secondDbComputer);
        }
    }
}
