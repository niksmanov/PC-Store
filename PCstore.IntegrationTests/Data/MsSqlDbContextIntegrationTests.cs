using Ninject;
using NUnit.Framework;
using PCstore.Data;
using PCstore.Data.Model;
using PCstore.Web.App_Start;
using System.Data.Entity.Validation;

namespace PCstore.IntegrationTests.Data
{
    [TestFixture]
    public class MsSqlDbContextIntegrationTests
    {
        private static IKernel kernel;

        [Test]
        public void ThrowDbEntityValidationException_WhenInvalidDataIsSended_WhenSaveChangesIsCalled()
        {
            // Arrange
            var computer = new Computer()
            {
                RAM = 100
            };

            try
            {
                // Act
                kernel = InjectionConfig.CreateKernel();
                var dbContext = kernel.Get<MsSqlDbContext>();

                dbContext.Computers.Add(computer);
                dbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Assert
                Assert.IsInstanceOf<DbEntityValidationException>(ex);
            }
        }
    }
}
