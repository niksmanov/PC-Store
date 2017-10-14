using AutoMapper;
using Moq;
using NUnit.Framework;
using PCstore.Data.Model;
using PCstore.Services.Contracts;
using PCstore.Web.Controllers;
using PCstore.Web.Models;
using PCstore.Web.Providers.Contracts;
using PCstore.Web.ViewModels.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.FluentMVCTesting;

namespace PCstore.UnitTests.Controllers
{
    [TestFixture]
    public class ManageControllerTests
    {
        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenProviderIsNull()
        {
            //Arrange
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            Assert.Throws<ArgumentNullException>(() =>
            new ManageController(null, mockedMapper.Object, mockedUsersService.Object, mockedComputersService.Object,
            mockedLaptopsService.Object, mockedDisplaysService.Object));
        }
        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenMapperIsNull()
        {
            //Arrange
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            Assert.Throws<ArgumentNullException>(() =>
            new ManageController(mockedProvider.Object, null, mockedUsersService.Object, mockedComputersService.Object,
            mockedLaptopsService.Object, mockedDisplaysService.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenUsersServiceIsNull()
        {
            //Arrange
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            Assert.Throws<ArgumentNullException>(() =>
            new ManageController(mockedProvider.Object, mockedMapper.Object, null, mockedComputersService.Object,
            mockedLaptopsService.Object, mockedDisplaysService.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenComputersServiceIsNull()
        {
            //Arrange
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            Assert.Throws<ArgumentNullException>(() =>
            new ManageController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object, null,
            mockedLaptopsService.Object, mockedDisplaysService.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenLaptopsServiceIsNull()
        {
            //Arrange
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            Assert.Throws<ArgumentNullException>(() =>
            new ManageController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, null, mockedDisplaysService.Object));
        }


        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenDisplaysServiceIsNull()
        {
            //Arrange
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            Assert.Throws<ArgumentNullException>(() =>
            new ManageController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, null));
        }

        [Test]
        public void Index_ShouldReturnsTrue_WhenComputers_AreValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Computer, ComputerViewModel>();
                cfg.CreateMap<ComputerViewModel, Computer>();

                cfg.CreateMap<Laptop, LaptopViewModel>();
                cfg.CreateMap<LaptopViewModel, Laptop>();

                cfg.CreateMap<Display, DisplayViewModel>();
                cfg.CreateMap<DisplayViewModel, Display>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new ManageController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            // Act
            var computer = new Computer();
            var laptop = new Laptop();
            var display = new Display();

            var computersCollection = new List<Computer>() { computer };
            var laptopsCollection = new List<Laptop>() { laptop };
            var displaysCollection = new List<Display>() { display };

            mockedComputersService.Setup(c => c.GetAll()).Returns(computersCollection.AsQueryable());

            mockedLaptopsService.Setup(c => c.GetAll()).Returns(laptopsCollection.AsQueryable());

            mockedDisplaysService.Setup(c => c.GetAll()).Returns(displaysCollection.AsQueryable());

            //Assert
            controller
                .WithCallTo(c => c.Index("Computer", computer.Id))
                .ShouldReturnJson();
        }


        [Test]
        public void Index_ShouldReturnsTrue_WhenLaptops_AreValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Computer, ComputerViewModel>();
                cfg.CreateMap<ComputerViewModel, Computer>();

                cfg.CreateMap<Laptop, LaptopViewModel>();
                cfg.CreateMap<LaptopViewModel, Laptop>();

                cfg.CreateMap<Display, DisplayViewModel>();
                cfg.CreateMap<DisplayViewModel, Display>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new ManageController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            // Act
            var computer = new Computer();
            var laptop = new Laptop();
            var display = new Display();

            var computersCollection = new List<Computer>() { computer };
            var laptopsCollection = new List<Laptop>() { laptop };
            var displaysCollection = new List<Display>() { display };

            mockedComputersService.Setup(c => c.GetAll()).Returns(computersCollection.AsQueryable());

            mockedLaptopsService.Setup(c => c.GetAll()).Returns(laptopsCollection.AsQueryable());

            mockedDisplaysService.Setup(c => c.GetAll()).Returns(displaysCollection.AsQueryable());

            //Assert
            controller
                .WithCallTo(c => c.Index("Laptop", laptop.Id))
                .ShouldReturnJson();
        }

        [Test]
        public void Index_ShouldReturnsTrue_WhenDisplays_AreValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Computer, ComputerViewModel>();
                cfg.CreateMap<ComputerViewModel, Computer>();

                cfg.CreateMap<Laptop, LaptopViewModel>();
                cfg.CreateMap<LaptopViewModel, Laptop>();

                cfg.CreateMap<Display, DisplayViewModel>();
                cfg.CreateMap<DisplayViewModel, Display>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new ManageController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            // Act
            var computer = new Computer();
            var laptop = new Laptop();
            var display = new Display();

            var computersCollection = new List<Computer>() { computer };
            var laptopsCollection = new List<Laptop>() { laptop };
            var displaysCollection = new List<Display>() { display };

            mockedComputersService.Setup(c => c.GetAll()).Returns(computersCollection.AsQueryable());

            mockedLaptopsService.Setup(c => c.GetAll()).Returns(laptopsCollection.AsQueryable());

            mockedDisplaysService.Setup(c => c.GetAll()).Returns(displaysCollection.AsQueryable());

            //Assert
            controller
                .WithCallTo(c => c.Index("Display", display.Id))
                .ShouldReturnJson();
        }

        [Test]
        public void ChangePasswordGET_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            //Arrange
            var controller = new ManageController();

            // Assert
            controller
                .WithCallTo(c => c.ChangePassword())
                .ShouldRenderView("ChangePassword");
        }

        [Test]
        public void ChangePasswordPOST_ShouldRenderView_WhenIsNotValid()
        {
            // Arrange
            var mockedVerification = new Mock<IVerificationProvider>();
            var controller = new ManageController();
            controller.ModelState.AddModelError("wrorng model", "Error");
            var viewModel = new ChangePasswordViewModel();

            // Act and Assert
            controller
                .WithCallTo(c => c.ChangePassword(viewModel))
                .ShouldRenderDefaultView();
        }
    }
}
