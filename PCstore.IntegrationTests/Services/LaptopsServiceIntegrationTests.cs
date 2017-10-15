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
    public class LaptopsServiceIntegrationTests
    {
        private static IKernel kernel;

        private static User dbUser = new User()
        {
            Id = Guid.NewGuid().ToString(),
            CreatedOn = DateTime.Now,
            ModifiedOn = DateTime.Now,
            Email = "stamat@abv.bg",
            UserName = "stamat@abv.bg"
        };

        private static Laptop dbLaptop = new Laptop()
        {
            DisplayResolution = "1920x1080",
            Battery = "Li-Pol",
            DisplaySize = 14,
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
            Email = "ivan@abv.bg",
            UserName = "ivan@abv.bg"
        };

        private static Laptop secondDbLaptop = new Laptop()
        {
            DisplayResolution = "1920x1080",
            Battery = "Li-Pol",
            DisplaySize = 14,
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

            dbContext.Laptops.Add(dbLaptop);
            dbContext.SaveChanges();
        }

        [OneTimeTearDown]
        public void TestCleanup()
        {
            var dbContext = kernel.Get<MsSqlDbContext>();

            foreach (var laptop in dbContext.Laptops)
            {
                dbContext.Laptops.Attach(laptop);
                dbContext.Laptops.Remove(laptop);
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
            var laptopsService = kernel.Get<ILaptopsService>();

            // Act
            var result = laptopsService.GetAll().Single(x => x.Id == dbLaptop.Id);

            // Assert
            Assert.IsNotNull(result);
        
            Assert.AreEqual(dbLaptop.DisplayResolution, result.DisplayResolution);
            Assert.AreEqual(dbLaptop.Battery, result.Battery);
            Assert.AreEqual(dbLaptop.DisplaySize, result.DisplaySize);
            Assert.AreEqual(dbLaptop.CPU, result.CPU);
            Assert.AreEqual(dbLaptop.CpuSpeed, result.CpuSpeed);
            Assert.AreEqual(dbLaptop.RAM, result.RAM);
            Assert.AreEqual(dbLaptop.RamType, result.RamType);
            Assert.AreEqual(dbLaptop.HDD, result.HDD);
            Assert.AreEqual(dbLaptop.GPU, result.GPU);
            Assert.AreEqual(dbLaptop.GpuMemory, result.GpuMemory);
            Assert.AreEqual(dbLaptop.OpticalDevice, result.OpticalDevice);
            Assert.AreEqual(dbLaptop.OperatingSystem, result.OperatingSystem);
            Assert.AreEqual(dbLaptop.Price, result.Price);
            Assert.AreEqual(dbLaptop.SellerPhone, result.SellerPhone);
            Assert.AreEqual(dbLaptop.Description, result.Description);
        }

        [Test]
        public void ReturnNull_WhenIdIsNull()
        {
            // Arrange
            var laptopsService = kernel.Get<ILaptopsService>();

            // Act
            var model = laptopsService.GetAll().SingleOrDefault(x => x.Id == null);

            // Assert
            Assert.IsNull(model);
        }

        [Test]
        public void ReturnNull_WhenThereIsNoModelWithThePassedId()
        {
            // Arrange
            var laptopsService = kernel.Get<ILaptopsService>();

            // Act
            var model = laptopsService.GetAll().SingleOrDefault(x => x.Id == Guid.NewGuid());

            // Assert
            Assert.IsNull(model);
        }

        [Test]
        public void LaptopsService_ShouldAddLaptop_IfValid()
        {
            // Arrange
            kernel = InjectionConfig.CreateKernel();
            var dbContext = kernel.Get<MsSqlDbContext>();

            var laptopsService = kernel.Get<ILaptopsService>();
            var laptopsCountBeforeAdding = laptopsService.GetAll().ToList().Count();

            // Act
            dbContext.Laptops.Add(secondDbLaptop);
            dbContext.SaveChanges();

            var laptopsCountAfterAdding = laptopsService.GetAll().ToList().Count();


            // Assert
            Assert.AreEqual(laptopsCountAfterAdding, laptopsCountBeforeAdding + 1);
        }
    }
}
