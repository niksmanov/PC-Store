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
    public class LaptopsServiceTests
    {
        [Test]
        public void Service_ShouldThrowArgumentNullException_WhenEfRepositoryIsNull()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<Laptop>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new LaptopsService(null, mockedUnitOfWork.Object));
        }

        [Test]
        public void Service_ShouldThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<Laptop>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new LaptopsService(mockedEfRepository.Object, null));
        }

        [Test]
        public void GetAll_ShouldReturnAllLaptopsFromDatabase_WhickAreNotDeleted()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<Laptop>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            mockedEfRepository.Setup(x => x.All);

            var service = new LaptopsService(mockedEfRepository.Object, mockedUnitOfWork.Object);

            // Act
            var result = service.GetAll();

            // Assert
            mockedEfRepository.Verify(x => x.All, Times.Once);
        }

        [Test]
        public void UpdateLaptop_ShouldUpdateLaptopInDatabase()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<Laptop>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var laptop = new Laptop();

            mockedEfRepository.Setup(x => x.Update(laptop));

            var service = new LaptopsService(mockedEfRepository.Object, mockedUnitOfWork.Object);


            // Act
            service.Update(laptop);

            // Assert
            mockedEfRepository.Verify(x => x.Update(laptop), Times.Once);
        }

        [Test]
        public void AddLaptop_ShouldAddLaptopToDatabase()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<Laptop>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var laptop = new Laptop();

            mockedEfRepository.Setup(x => x.Add(laptop));

            var service = new LaptopsService(mockedEfRepository.Object, mockedUnitOfWork.Object);


            // Act
            service.Add(laptop);

            // Assert
            mockedEfRepository.Verify(x => x.Add(laptop), Times.Once);
        }

        [Test]
        public void DeleteLaptop_ShouldDeleteLaptopInDatabase()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<Laptop>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var laptop = new Laptop();

            mockedEfRepository.Setup(x => x.Delete(laptop));

            var service = new LaptopsService(mockedEfRepository.Object, mockedUnitOfWork.Object);


            // Act
            service.Delete(laptop);

            // Assert
            mockedEfRepository.Verify(x => x.Delete(laptop), Times.Once);
        }
    }
}
