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
    public class ComputersServiceTests
    {
        [Test]
        public void Service_ShouldThrowArgumentNullException_WhenEfRepositoryIsNull()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<Computer>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new ComputersService(null, mockedUnitOfWork.Object));
        }

        [Test]
        public void Service_ShouldThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<Computer>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new ComputersService(mockedEfRepository.Object, null));
        }

        [Test]
        public void GetAll_ShouldReturnAllComputersFromDatabase_WhickAreNotDeleted()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<Computer>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            mockedEfRepository.Setup(x => x.All);

            var service = new ComputersService(mockedEfRepository.Object, mockedUnitOfWork.Object);

            // Act
            var result = service.GetAll();

            // Assert
            mockedEfRepository.Verify(x => x.All, Times.Once);
        }

        [Test]
        public void UpdateComputer_ShouldUpdateComputerInDatabase()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<Computer>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var computer = new Computer();


            mockedEfRepository.Setup(x => x.Update(computer));

            var service = new ComputersService(mockedEfRepository.Object, mockedUnitOfWork.Object);


            // Act
            service.Update(computer);

            // Assert
            mockedEfRepository.Verify(x => x.Update(computer), Times.Once);
        }

        [Test]
        public void AddComputer_ShouldAddComputerToDatabase()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<Computer>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var computer = new Computer();

            mockedEfRepository.Setup(x => x.Add(computer));

            var service = new ComputersService(mockedEfRepository.Object, mockedUnitOfWork.Object);


            // Act
            service.Add(computer);

            // Assert
            mockedEfRepository.Verify(x => x.Add(computer), Times.Once);
        }

        [Test]
        public void DeleteComputer_ShouldDeleteComputerInDatabase()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<Computer>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var computer = new Computer();

            mockedEfRepository.Setup(x => x.Delete(computer));

            var service = new ComputersService(mockedEfRepository.Object, mockedUnitOfWork.Object);

            // Act
            service.Delete(computer);

            // Assert
            mockedEfRepository.Verify(x => x.Delete(computer), Times.Once);
        }
    }
}
