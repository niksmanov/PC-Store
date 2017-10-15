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
    public class DisplaysServiceIntegrationTests
    {
        private static IKernel kernel;

        private static User dbUser = new User()
        {
            Id = Guid.NewGuid().ToString(),
            CreatedOn = DateTime.Now,
            ModifiedOn = DateTime.Now,
            Email = "petkan@abv.bg",
            UserName = "petkan@abv.bg"
        };

        private static Display dbDisplay = new Display()
        {
            Colors = 12000000,
            Resolution = "1920x1080",
            Size = 14,
            Type = "IPS",
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
            Email = "batman@abv.bg",
            UserName = "batman@abv.bg"
        };

        private static Display secondbDisplay = new Display()
        {
            Colors = 12000000,
            Resolution = "1920x1080",
            Size = 14,
            Type = "IPS",
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

            dbContext.Displays.Add(dbDisplay);
            dbContext.SaveChanges();
        }

        [OneTimeTearDown]
        public void TestCleanup()
        {
            var dbContext = kernel.Get<MsSqlDbContext>();

            foreach (var display in dbContext.Displays)
            {
                dbContext.Displays.Attach(display);
                dbContext.Displays.Remove(display);
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
            var displaysService = kernel.Get<IDisplaysService>();

            // Act
            var result = displaysService.GetAll().Single(x => x.Id == dbDisplay.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(dbDisplay.Colors, result.Colors);
            Assert.AreEqual(dbDisplay.Resolution, result.Resolution);
            Assert.AreEqual(dbDisplay.Size, result.Size);
            Assert.AreEqual(dbDisplay.Type, result.Type);
            Assert.AreEqual(dbDisplay.Price, result.Price);
            Assert.AreEqual(dbDisplay.SellerPhone, result.SellerPhone);
            Assert.AreEqual(dbDisplay.Description, result.Description);
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
        public void DisplaysService_ShouldAddDisplay_IfValid()
        {
            // Arrange
            kernel = InjectionConfig.CreateKernel();
            var dbContext = kernel.Get<MsSqlDbContext>();

            var displaysService = kernel.Get<IDisplaysService>();
            var displaysCountBeforeAdding = displaysService.GetAll().ToList().Count();

            // Act
            dbContext.Displays.Add(secondbDisplay);
            dbContext.SaveChanges();

            var displaysCountAfterAdding = displaysService.GetAll().ToList().Count();


            // Assert
            Assert.AreEqual(displaysCountAfterAdding, displaysCountBeforeAdding + 1);
        }
    }
}
