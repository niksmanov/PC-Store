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
    public class UsersServiceIntegrationTests
    {
        private static IKernel kernel;

        private static User dbUser = new User()
        {
            Id = Guid.NewGuid().ToString(),
            CreatedOn = DateTime.Now,
            ModifiedOn = DateTime.Now,
            Email = "superman@abv.bg",
            UserName = "superman@abv.bg"
        };

        private static User secondDbUser = new User()
        {
            Id = Guid.NewGuid().ToString(),
            CreatedOn = DateTime.Now,
            ModifiedOn = DateTime.Now,
            Email = "spiderman@abv.bg",
            UserName = "spiderman@abv.bg"
        };

        [OneTimeSetUp]
        public void TestInit()
        {
            kernel = InjectionConfig.CreateKernel();
            var dbContext = kernel.Get<MsSqlDbContext>();

            dbContext.Users.Add(dbUser);
            dbContext.SaveChanges();
        }

        [OneTimeTearDown]
        public void TestCleanup()
        {
            var dbContext = kernel.Get<MsSqlDbContext>();

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
            var usersService = kernel.Get<IUsersService>();

            // Act
            var result = usersService.GetAll().Single(x => x.Id == dbUser.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(dbUser.Email, result.Email);
            Assert.AreEqual(dbUser.Id, result.Id);
        }

        [Test]
        public void ReturnNull_WhenIdIsNull()
        {
            // Arrange
            var usersService = kernel.Get<IUsersService>();

            // Act
            var model = usersService.GetAll().SingleOrDefault(x => x.Id == null);

            // Assert
            Assert.IsNull(model);
        }

        [Test]
        public void ReturnNull_WhenThereIsNoModelWithThePassedId()
        {
            // Arrange
            var usersService = kernel.Get<IUsersService>();

            // Act
            var model = usersService.GetAll().SingleOrDefault(x => x.Id == Guid.NewGuid().ToString());

            // Assert
            Assert.IsNull(model);
        }

        [Test]
        public void UsersService_ShouldAddUser_IfValid()
        {
            // Arrange
            kernel = InjectionConfig.CreateKernel();
            var dbContext = kernel.Get<MsSqlDbContext>();

            var usersService = kernel.Get<IUsersService>();
            var usersCountBeforeAdding = usersService.GetAll().ToList().Count();

            // Act
            dbContext.Users.Add(secondDbUser);
            dbContext.SaveChanges();

            var usersCountAfterAdding = usersService.GetAll().ToList().Count();


            // Assert
            Assert.AreEqual(usersCountAfterAdding, usersCountBeforeAdding + 1);
        }
    }
}
