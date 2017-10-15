using Ninject;
using NUnit.Framework;
using PCstore.Data;
using PCstore.Data.Model;
using PCstore.Services.Contracts;
using PCstore.Web.App_Start;
using System;
using System.Linq;

namespace PCstore.IntegrationTests.Services
{
    [TestFixture]
    public class ComputersServiceIntegrationTests
    {
        private static IKernel kernel;

        private static User dbUser = new User()
        {
            Id = Guid.NewGuid().ToString(),
            CreatedOn = DateTime.Now,
            ModifiedOn = DateTime.Now,
            Email = "pesho@abv.bg",
            UserName = "pesho@abv.bg"
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
            Email = "gosho@abv.bg",
            UserName = "gosho@abv.bg"
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
        public void ReturnModelWithCorrectProperties_WhenThereIsAModelWithThePassedId()
        {
            // Arrange
            var computersService = kernel.Get<IComputersService>();

            // Act
            var result = computersService.GetAll().Single(x => x.Id == dbComputer.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(dbComputer.CPU, result.CPU);
            Assert.AreEqual(dbComputer.CpuSpeed, result.CpuSpeed);
            Assert.AreEqual(dbComputer.RAM, result.RAM);
            Assert.AreEqual(dbComputer.RamType, result.RamType);
            Assert.AreEqual(dbComputer.HDD, result.HDD);
            Assert.AreEqual(dbComputer.GPU, result.GPU);
            Assert.AreEqual(dbComputer.GpuMemory, result.GpuMemory);
            Assert.AreEqual(dbComputer.OpticalDevice, result.OpticalDevice);
            Assert.AreEqual(dbComputer.OperatingSystem, result.OperatingSystem);
            Assert.AreEqual(dbComputer.Price, result.Price);
            Assert.AreEqual(dbComputer.SellerPhone, result.SellerPhone);
            Assert.AreEqual(dbComputer.Description, result.Description);
        }

        [Test]
        public void ReturnNull_WhenIdIsNull()
        {
            // Arrange
            var computersService = kernel.Get<IComputersService>();

            // Act
            var model = computersService.GetAll().SingleOrDefault(x => x.Id == null);

            // Assert
            Assert.IsNull(model);
        }

        [Test]
        public void ReturnNull_WhenThereIsNoModelWithThePassedId()
        {
            // Arrange
            var computersService = kernel.Get<IComputersService>();

            // Act
            var model = computersService.GetAll().SingleOrDefault(x => x.Id == Guid.NewGuid());

            // Assert
            Assert.IsNull(model);
        }

        [Test]
        public void ComputersService_ShouldAddComputer_IfValid()
        {
            // Arrange
            kernel = InjectionConfig.CreateKernel();
            var dbContext = kernel.Get<MsSqlDbContext>();

            var computersService = kernel.Get<IComputersService>();
            var computersCountBeforeAdding = computersService.GetAll().ToList().Count();

            // Act
            dbContext.Computers.Add(secondDbComputer);
            dbContext.SaveChanges();

            var computersCountAfterAdding = computersService.GetAll().ToList().Count();


            // Assert
            Assert.AreEqual(computersCountAfterAdding, computersCountBeforeAdding + 1);
        }
    }
}
