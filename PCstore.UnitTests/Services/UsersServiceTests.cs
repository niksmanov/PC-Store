using Moq;
using NUnit.Framework;
using PCstore.Data.Model;
using PCstore.Data.Repositories;
using PCstore.Data.UnitOfWork;
using PCstore.Services;
using System;

namespace PCstore.UnitTests.Services
{
    [TestFixture]
    public class UsersServiceTests
    {
        [Test]
        public void Service_ShouldThrowArgumentNullException_WhenEfRepositoryIsNull()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<User>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new UsersService(null, mockedUnitOfWork.Object));
        }

        [Test]
        public void Service_ShouldThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<User>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new UsersService(mockedEfRepository.Object, null));
        }

        [Test]
        public void GetAll_ShouldReturnAllUsersFromDatabase_WhickAreNotDeleted()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<User>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            mockedEfRepository.Setup(x => x.All);

            var service = new UsersService(mockedEfRepository.Object, mockedUnitOfWork.Object);

            // Act
            var result = service.GetAll();

            // Assert
            mockedEfRepository.Verify(x => x.All, Times.Once);
        }

        [Test]
        public void UpdateUser_ShouldUpdateUserInDatabase()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<User>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var user = new User();

            mockedEfRepository.Setup(x => x.Update(user));

            var service = new UsersService(mockedEfRepository.Object, mockedUnitOfWork.Object);


            // Act
            service.Update(user);

            // Assert
            mockedEfRepository.Verify(x => x.Update(user), Times.Once);
        }

        [Test]
        public void DeleteUser_ShouldDeleteUserInDatabase()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<User>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var user = new User();

            mockedEfRepository.Setup(x => x.Delete(user));

            var service = new UsersService(mockedEfRepository.Object, mockedUnitOfWork.Object);


            // Act
            service.Delete(user);

            // Assert
            mockedEfRepository.Verify(x => x.Delete(user), Times.Once);
        }
    }
}
