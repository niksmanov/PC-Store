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
    public class DisplaysServiceTests
    {
        [Test]
        public void Service_ShouldThrowArgumentNullException_WhenEfRepositoryIsNull()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<Display>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new DisplaysService(null, mockedUnitOfWork.Object));
        }

        [Test]
        public void Service_ShouldThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<Display>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new DisplaysService(mockedEfRepository.Object, null));
        }

        [Test]
        public void GetAll_ShouldReturnAllDisplaysFromDatabase_WhickAreNotDeleted()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<Display>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            mockedEfRepository.Setup(x => x.All);

            var service = new DisplaysService(mockedEfRepository.Object, mockedUnitOfWork.Object);

            // Act
            var result = service.GetAll();

            // Assert
            mockedEfRepository.Verify(x => x.All, Times.Once);
        }

        [Test]
        public void UpdateDisplay_ShouldUpdateDisplayInDatabase()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<Display>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var display = new Display();

            mockedEfRepository.Setup(x => x.Update(display));

            var service = new DisplaysService(mockedEfRepository.Object, mockedUnitOfWork.Object);


            // Act
            service.Update(display);

            // Assert
            mockedEfRepository.Verify(x => x.Update(display), Times.Once);
        }

        [Test]
        public void AddDisplay_ShouldAddDisplayToDatabase()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<Display>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var display = new Display();

            mockedEfRepository.Setup(x => x.Add(display));

            var service = new DisplaysService(mockedEfRepository.Object, mockedUnitOfWork.Object);


            // Act
            service.Add(display);

            // Assert
            mockedEfRepository.Verify(x => x.Add(display), Times.Once);
        }

        [Test]
        public void DeleteDisplay_ShouldDeleteDisplayInDatabase()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<Display>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var display = new Display();

            mockedEfRepository.Setup(x => x.Delete(display));

            var service = new DisplaysService(mockedEfRepository.Object, mockedUnitOfWork.Object);


            // Act
            service.Delete(display);

            // Assert
            mockedEfRepository.Verify(x => x.Delete(display), Times.Once);
        }
    }
}
