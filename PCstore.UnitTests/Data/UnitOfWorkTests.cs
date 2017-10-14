using Moq;
using NUnit.Framework;
using PCstore.Data;
using PCstore.Data.UnitOfWork;
using System;

namespace PCstore.UnitTests.Data
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        [Test]
        public void Controller_ShouldThrowsArgumentNullException_WhenPassedParametersAreNull()
        {
            var mockedDbContext = new Mock<MsSqlDbContext>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new UnitOfWork(null));
        }

        [Test]
        public void Controller_ShouldNotThrowArgumentNullException_WhenPassedParametersAreNull()
        {
            var mockedDbContext = new Mock<MsSqlDbContext>();

            //Act and Assert
            Assert.DoesNotThrow(() => new UnitOfWork(mockedDbContext.Object));
        }

        [Test]
        public void Commit_ShouldCallSaveChangesToDatabaseContext()
        {
            // Arrange
            var mockedDbContext = new Mock<MsSqlDbContext>();
            mockedDbContext.Setup(x => x.SaveChanges());

            var saveContext = new UnitOfWork(mockedDbContext.Object);

            // Act
            saveContext.Commit();

            // Assert
            mockedDbContext.Verify(dbc => dbc.SaveChanges(), Times.Once);
        }

    }
}
