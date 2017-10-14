using AutoMapper;
using Moq;
using NUnit.Framework;
using PCstore.Data.Model;
using PCstore.Services.Contracts;
using PCstore.Web.Controllers;
using PCstore.Web.Providers.Contracts;
using PCstore.Web.ViewModels.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.FluentMVCTesting;

namespace PCstore.UnitTests.Controllers
{
    [TestFixture]
    public class DeviceControllerTests
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
            new DeviceController(null, mockedMapper.Object, mockedUsersService.Object, mockedComputersService.Object,
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
            new DeviceController(mockedProvider.Object, null, mockedUsersService.Object, mockedComputersService.Object,
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
            new DeviceController(mockedProvider.Object, mockedMapper.Object, null, mockedComputersService.Object,
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
            new DeviceController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object, null,
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
            new DeviceController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
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
            new DeviceController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, null));
        }

        [Test]
        public void Computers_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Computer, ComputerViewModel>();
                cfg.CreateMap<ComputerViewModel, Computer>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new DeviceController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            //Act and Assert
            controller
                .WithCallTo(c => c.Computers(1))
                .ShouldRenderView("Computers");
        }

        [Test]
        public void Computer_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Computer, ComputerViewModel>();
                cfg.CreateMap<ComputerViewModel, Computer>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new DeviceController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            // Act
            var computer = new Computer
            {
                Id = Guid.NewGuid()
            };
            var computersCollection = new List<Computer>() { computer };

            mockedComputersService.Setup(c => c.GetAll()).Returns(computersCollection.AsQueryable());


            //Assert
            controller
                .WithCallTo(c => c.Computer(computer.Id))
                .ShouldRenderView("Computer");
        }


        [Test]
        public void AddComputerGET_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new DeviceController();

            // Act and Assert
            controller
                .WithCallTo(c => c.AddComputer())
                .ShouldRenderView("AddComputer");
        }

        [Test]
        public void AddComputerPOST_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Computer, ComputerViewModel>();
                cfg.CreateMap<ComputerViewModel, Computer>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new DeviceController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            // Act
            var computer = new Computer
            {
                Id = Guid.NewGuid()
            };

            var user = new User
            {
                Id = "123"
            };

            var usersCollection = new List<User>() { user };

            mockedProvider.Setup(x => x.CurrentUserId).Returns(user.Id);
            mockedUsersService.Setup(c => c.GetAll()).Returns(usersCollection.AsQueryable());

            mockedComputersService.Setup(c => c.Add(computer));

            //Assert
            controller
                .WithCallTo(c => c.AddComputer(computer))
                .ShouldRedirectTo((ManageController c) => c.Index(null, 1));
        }

        [Test]
        public void UpdateComputerGET_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Computer, ComputerViewModel>();
                cfg.CreateMap<ComputerViewModel, Computer>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new DeviceController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            // Act
            var computer = new Computer
            {
                Id = Guid.NewGuid()
            };
            var computersCollection = new List<Computer>() { computer };

            mockedComputersService.Setup(c => c.GetAll()).Returns(computersCollection.AsQueryable());


            //Assert
            controller
                .WithCallTo(c => c.UpdateComputer(computer.Id))
                .ShouldRenderView("UpdateComputer");
        }

        [Test]
        public void UpdateComputerPOST_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Computer, ComputerViewModel>();
                cfg.CreateMap<ComputerViewModel, Computer>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new DeviceController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            // Act
            var computer = new Computer
            {
                Id = Guid.NewGuid()
            };

            mockedComputersService.Setup(c => c.Update(computer));

            //Assert
            controller
                .WithCallTo(c => c.UpdateComputer(computer))
                .ShouldRedirectTo((ManageController c) => c.Index(null, 1));
        }


        [Test]
        public void Laptops_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Laptop, LaptopViewModel>();
                cfg.CreateMap<LaptopViewModel, Laptop>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new DeviceController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            //Act and Assert
            controller
                .WithCallTo(c => c.Laptops(1))
                .ShouldRenderView("Laptops");
        }

        [Test]
        public void Laptop_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Laptop, LaptopViewModel>();
                cfg.CreateMap<LaptopViewModel, Laptop>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new DeviceController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            // Act
            var laptop = new Laptop
            {
                Id = Guid.NewGuid()
            };
            var laptopsCollection = new List<Laptop>() { laptop };

            mockedLaptopsService.Setup(c => c.GetAll()).Returns(laptopsCollection.AsQueryable());


            //Assert
            controller
                .WithCallTo(c => c.Laptop(laptop.Id))
                .ShouldRenderView("Laptop");
        }


        [Test]
        public void AddLaptopGET_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new DeviceController();

            // Act and Assert
            controller
                .WithCallTo(c => c.AddLaptop())
                .ShouldRenderView("AddLaptop");
        }

        [Test]
        public void AddLaptopPOST_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Laptop, LaptopViewModel>();
                cfg.CreateMap<LaptopViewModel, Laptop>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new DeviceController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            // Act
            var laptop = new Laptop
            {
                Id = Guid.NewGuid()
            };

            var user = new User
            {
                Id = "123"
            };

            var usersCollection = new List<User>() { user };

            mockedProvider.Setup(x => x.CurrentUserId).Returns(user.Id);
            mockedUsersService.Setup(c => c.GetAll()).Returns(usersCollection.AsQueryable());

            mockedLaptopsService.Setup(c => c.Add(laptop));

            //Assert
            controller
                .WithCallTo(c => c.AddLaptop(laptop))
                .ShouldRedirectTo((ManageController c) => c.Index(null, 1));
        }

        [Test]
        public void UpdateLaptopGET_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Laptop, LaptopViewModel>();
                cfg.CreateMap<LaptopViewModel, Laptop>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new DeviceController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            // Act
            var laptop = new Laptop
            {
                Id = Guid.NewGuid()
            };
            var laptopsCollection = new List<Laptop>() { laptop };

            mockedLaptopsService.Setup(c => c.GetAll()).Returns(laptopsCollection.AsQueryable());


            //Assert
            controller
                .WithCallTo(c => c.UpdateLaptop(laptop.Id))
                .ShouldRenderView("UpdateLaptop");
        }

        [Test]
        public void UpdateLaptopPOST_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Laptop, LaptopViewModel>();
                cfg.CreateMap<LaptopViewModel, Laptop>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new DeviceController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            // Act
            var laptop = new Laptop
            {
                Id = Guid.NewGuid()
            };

            mockedLaptopsService.Setup(c => c.Update(laptop));

            //Assert
            controller
                .WithCallTo(c => c.UpdateLaptop(laptop))
                .ShouldRedirectTo((ManageController c) => c.Index(null, 1));
        }

        [Test]
        public void Displays_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Display, DisplayViewModel>();
                cfg.CreateMap<DisplayViewModel, Display>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new DeviceController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            //Act and Assert
            controller
                .WithCallTo(c => c.Displays(1))
                .ShouldRenderView("Displays");
        }

        [Test]
        public void Display_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Display, DisplayViewModel>();
                cfg.CreateMap<DisplayViewModel, Display>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new DeviceController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            // Act
            var display = new Display
            {
                Id = Guid.NewGuid()
            };
            var displaysCollection = new List<Display>() { display };

            mockedDisplaysService.Setup(c => c.GetAll()).Returns(displaysCollection.AsQueryable());


            //Assert
            controller
                .WithCallTo(c => c.Display(display.Id))
                .ShouldRenderView("Display");
        }


        [Test]
        public void AddDisplayGET_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new DeviceController();

            // Act and Assert
            controller
                .WithCallTo(c => c.AddDisplay())
                .ShouldRenderView("AddDisplay");
        }

        [Test]
        public void AddDisplayPOST_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Display, DisplayViewModel>();
                cfg.CreateMap<DisplayViewModel, Display>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new DeviceController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            // Act
            var display = new Display
            {
                Id = Guid.NewGuid()
            };

            var user = new User
            {
                Id = "123"
            };

            var usersCollection = new List<User>() { user };

            mockedProvider.Setup(x => x.CurrentUserId).Returns(user.Id);
            mockedUsersService.Setup(c => c.GetAll()).Returns(usersCollection.AsQueryable());

            mockedDisplaysService.Setup(c => c.Add(display));

            //Assert
            controller
                .WithCallTo(c => c.AddDisplay(display))
                .ShouldRedirectTo((ManageController c) => c.Index(null, 1));
        }

        [Test]
        public void UpdateDisplayGET_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Display, DisplayViewModel>();
                cfg.CreateMap<DisplayViewModel, Display>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new DeviceController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            // Act
            var display = new Display
            {
                Id = Guid.NewGuid()
            };
            var displaysCollection = new List<Display>() { display };

            mockedDisplaysService.Setup(c => c.GetAll()).Returns(displaysCollection.AsQueryable());


            //Assert
            controller
                .WithCallTo(c => c.UpdateDisplay(display.Id))
                .ShouldRenderView("UpdateDisplay");
        }

        [Test]
        public void UpdateDisplayPOST_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Display, DisplayViewModel>();
                cfg.CreateMap<DisplayViewModel, Display>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new DeviceController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            // Act
            var display = new Display
            {
                Id = Guid.NewGuid()
            };

            mockedDisplaysService.Setup(c => c.Update(display));

            //Assert
            controller
                .WithCallTo(c => c.UpdateDisplay(display))
                .ShouldRedirectTo((ManageController c) => c.Index(null, 1));
        }

    }
}
