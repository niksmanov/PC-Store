using Moq;
using NUnit.Framework;
using PCstore.Data;
using PCstore.Data.Model;
using PCstore.Data.Repositories;
using PCstore.Data.UnitOfWork;
using PCstore.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace PCstore.UnitTests.Data
{
    [TestFixture]
    public class EfRepositoryTests
    {
        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }

        [Test]
        public void Controller_ShouldThrowsArgumentNullException_WhenPassedParametersAreNull()
        {
            var mockedDbContext = new Mock<MsSqlDbContext>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new EfRepository<Computer>(null));
        }


        [Test]
        public void Controller_ShouldNotThrowArgumentNullException_WhenPassedParametersAreNull()
        {
            var mockedDbContext = new Mock<MsSqlDbContext>();

            //Act and Assert
            Assert.DoesNotThrow(() => new EfRepository<Computer>(mockedDbContext.Object));
        }

        [Test]
        public void All_ShouldReturnsNotDeletedObjects_IfValid()
        {
            // Arrange
            var notDeletedComputer = new Computer { IsDeleted = false };
            var DeletedComputer = new Computer { IsDeleted = true };

            var computers = new List<Computer>()
            {
                notDeletedComputer,
                DeletedComputer
            };

            var computersDbSet = GetQueryableMockDbSet(computers);

            var mockedContext = new Mock<MsSqlDbContext>();
            mockedContext.Setup(c => c.Set<Computer>()).Returns(computersDbSet);

            var repository = new EfRepository<Computer>(mockedContext.Object);

            // Act
            var result = repository.All;

            // Assert
            Assert.AreEqual(1, result.ToList().Count);
        }

        [Test]
        public void All_ShouldReturnsAllObjects_IfValid()
        {
            // Arrange
            var notDeletedComputer = new Computer { IsDeleted = false };
            var DeletedComputer = new Computer { IsDeleted = true };

            var computers = new List<Computer>()
            {
                notDeletedComputer,
                DeletedComputer
            };

            var computersDbSet = GetQueryableMockDbSet(computers);

            var mockedContext = new Mock<MsSqlDbContext>();
            mockedContext.Setup(c => c.Set<Computer>()).Returns(computersDbSet);

            var repository = new EfRepository<Computer>(mockedContext.Object);

            // Act
            var result = repository.AllAndDeleted;

            // Assert
            Assert.AreEqual(2, result.ToList().Count);
        }
    }
}
