using AutoMapper;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using PCstore.Data.Model;
using PCstore.Services.Contracts;
using PCstore.Web.Areas.Administration.Controllers;
using PCstore.Web.Models;
using PCstore.Web.Providers.Contracts;
using PCstore.Web.ViewModels.Device;
using PCstore.Web.ViewModels.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.FluentMVCTesting;

namespace PCstore.UnitTests.Controllers
{
    [TestFixture]
    public class AdminPanelControllerTests
    {
        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenHttpContextIsNull()
        {
            //Arrange
            var mockedHttpContext = new Mock<IHttpContextProvider>();
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            Assert.Throws<ArgumentNullException>(() =>
            new AdminPanelController(null, mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenProviderIsNull()
        {
            //Arrange
            var mockedHttpContext = new Mock<IHttpContextProvider>();
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            Assert.Throws<ArgumentNullException>(() =>
            new AdminPanelController(mockedHttpContext.Object, null, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenMapperIsNull()
        {
            //Arrange
            var mockedHttpContext = new Mock<IHttpContextProvider>();
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            Assert.Throws<ArgumentNullException>(() =>
            new AdminPanelController(mockedHttpContext.Object, mockedProvider.Object, null, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenUsersServiceIsNull()
        {
            //Arrange
            var mockedHttpContext = new Mock<IHttpContextProvider>();
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            Assert.Throws<ArgumentNullException>(() =>
            new AdminPanelController(mockedHttpContext.Object, mockedProvider.Object, mockedMapper.Object, null,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenComputersServiceIsNull()
        {
            //Arrange
            var mockedHttpContext = new Mock<IHttpContextProvider>();
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            Assert.Throws<ArgumentNullException>(() =>
            new AdminPanelController(mockedHttpContext.Object, mockedProvider.Object, mockedMapper.Object,
            mockedUsersService.Object, null, mockedLaptopsService.Object, mockedDisplaysService.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenLaptopsServiceIsNull()
        {
            //Arrange
            var mockedHttpContext = new Mock<IHttpContextProvider>();
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            Assert.Throws<ArgumentNullException>(() =>
            new AdminPanelController(mockedHttpContext.Object, mockedProvider.Object, mockedMapper.Object,
            mockedUsersService.Object, mockedComputersService.Object, null, mockedDisplaysService.Object));
        }


        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenDisplaysServiceIsNull()
        {
            //Arrange
            var mockedHttpContext = new Mock<IHttpContextProvider>();
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            Assert.Throws<ArgumentNullException>(() =>
            new AdminPanelController(mockedHttpContext.Object, mockedProvider.Object, mockedMapper.Object,
            mockedUsersService.Object, mockedComputersService.Object, mockedLaptopsService.Object, null));
        }

        [Test]
        public void Index_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new AdminPanelController();

            // Act and Assert
            controller
                .WithCallTo(c => c.Index())
                .ShouldRenderView("Index");
        }

        [Test]
        public void AddUserGET_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new AdminPanelController();

            // Act and Assert
            controller
                .WithCallTo(c => c.AddUser())
                .ShouldRenderPartialView("AddUser");
        }

        [Test]
        public void AddUserPOST_ShouldRegister_NewUser()
        {
            // Arrange
            var mockedHttpContext = new Mock<IHttpContextProvider>();
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var mockedUser = new Mock<User>();
            var model = new User()
            {
                Email = "pesho@abv.bg"
            };

            var viewModel = new RegisterViewModel()
            {
                Email = "pesho@abv.bg"
            };

            mockedProvider.Setup(v => v.Register(model, "pesho")).Returns(IdentityResult.Success);

            var controller = new AdminPanelController(mockedHttpContext.Object, mockedProvider.Object, mockedMapper.Object,
            mockedUsersService.Object, mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            controller.AddUser(viewModel);

            // Act and Assert
            controller
                .WithCallTo(c => c.AddUser(viewModel))
                .ShouldRedirectTo(c => c.Index());
        }

        [Test]
        public void Users_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<UserViewModel, User>();
            });
            var mockedHttpContext = new Mock<IHttpContextProvider>();
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new AdminPanelController(mockedHttpContext.Object, mockedProvider.Object, mockedMapper.Object,
            mockedUsersService.Object, mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            var user = new User
            {
                Id = "123"
            };

            var usersCollection = new List<User>() { user };

            mockedProvider.Setup(x => x.CurrentUserId).Returns(user.Id);
            mockedUsersService.Setup(c => c.GetAll()).Returns(usersCollection.AsQueryable());


            // Act and Assert
            controller
                .WithCallTo(c => c.Users())
                .ShouldRenderPartialView("Users");
        }

        [Test]
        public void UpdateUserGET_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<UserViewModel, User>();
            });

            var mockedHttpContext = new Mock<IHttpContextProvider>();
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new AdminPanelController(mockedHttpContext.Object, mockedProvider.Object, mockedMapper.Object,
            mockedUsersService.Object, mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            var user = new User
            {
                Id = "123"
            };

            var usersCollection = new List<User>() { user };

            mockedProvider.Setup(x => x.CurrentUserId).Returns(user.Id);
            mockedUsersService.Setup(c => c.GetAll()).Returns(usersCollection.AsQueryable());


            // Act and Assert
            controller
                .WithCallTo(c => c.UpdateUser(user.Id))
                .ShouldRenderView("UpdateUser");
        }

        [Test]
        public void UpdateUserPOST_ShouldReturnsTrue_IfUser_IsInRoleUser()
        {
            // Arrange
            var mockedHttpContext = new Mock<IHttpContextProvider>();
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new AdminPanelController(mockedHttpContext.Object, mockedProvider.Object, mockedMapper.Object,
            mockedUsersService.Object, mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            // Act
            var user = new User
            {
                Id = "123"
            };

            var viewModel = new UserViewModel
            {
                Id = "123",
                Role = "Admin"
            };

            mockedMapper.Setup(x => x.Map<User>(viewModel)).Returns(user);
            
            mockedUsersService.Setup(c => c.Update(user));
            mockedProvider.Setup(x => x.AddToRole(user.Id, "Admin"));
            mockedProvider.Setup(x => x.RemoveFromRole(user.Id, "User"));

            //Assert
            controller
                .WithCallTo(c => c.UpdateUser(viewModel))
                .ShouldRedirectTo(c => c.Index());
        }

        [Test]
        public void UpdateUserPOST_ShouldReturnsTrue_IfUser_IsInRoleAdmin()
        {
            // Arrange
            var mockedHttpContext = new Mock<IHttpContextProvider>();
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new AdminPanelController(mockedHttpContext.Object, mockedProvider.Object, mockedMapper.Object,
            mockedUsersService.Object, mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            // Act
            var user = new User
            {
                Id = "123"
            };

            var viewModel = new UserViewModel
            {
                Id = "123",
                Role = "User"
            };

            mockedMapper.Setup(x => x.Map<User>(viewModel)).Returns(user);

            mockedUsersService.Setup(c => c.Update(user));
            mockedProvider.Setup(x => x.AddToRole(user.Id, "User"));
            mockedProvider.Setup(x => x.RemoveFromRole(user.Id, "Admin"));

            //Assert
            controller
                .WithCallTo(c => c.UpdateUser(viewModel))
                .ShouldRedirectTo(c => c.Index());
           
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

                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<UserViewModel, User>();
            });

            var mockedHttpContext = new Mock<IHttpContextProvider>();
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new AdminPanelController(mockedHttpContext.Object, mockedProvider.Object, mockedMapper.Object,
                mockedUsersService.Object, mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            // Act
            var computer = new Computer();
            var laptop = new Laptop();
            var display = new Display();
            var user = new User();

            var computersCollection = new List<Computer>() { computer };
            var laptopsCollection = new List<Laptop>() { laptop };
            var displaysCollection = new List<Display>() { display };
            var userssCollection = new List<User>() { user };


            mockedComputersService.Setup(c => c.GetAll()).Returns(computersCollection.AsQueryable());

            mockedLaptopsService.Setup(c => c.GetAll()).Returns(laptopsCollection.AsQueryable());

            mockedDisplaysService.Setup(c => c.GetAll()).Returns(displaysCollection.AsQueryable());

            mockedUsersService.Setup(c => c.GetAll()).Returns
                (userssCollection.AsQueryable());

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

                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<UserViewModel, User>();
            });

            var mockedHttpContext = new Mock<IHttpContextProvider>();
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new AdminPanelController(mockedHttpContext.Object, mockedProvider.Object, mockedMapper.Object,
                mockedUsersService.Object, mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            // Act
            var computer = new Computer();
            var laptop = new Laptop();
            var display = new Display();
            var user = new User();

            var computersCollection = new List<Computer>() { computer };
            var laptopsCollection = new List<Laptop>() { laptop };
            var displaysCollection = new List<Display>() { display };
            var userssCollection = new List<User>() { user };


            mockedComputersService.Setup(c => c.GetAll()).Returns(computersCollection.AsQueryable());

            mockedLaptopsService.Setup(c => c.GetAll()).Returns(laptopsCollection.AsQueryable());

            mockedDisplaysService.Setup(c => c.GetAll()).Returns(displaysCollection.AsQueryable());

            mockedUsersService.Setup(c => c.GetAll()).Returns
                (userssCollection.AsQueryable());

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

                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<UserViewModel, User>();
            });

            var mockedHttpContext = new Mock<IHttpContextProvider>();
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new AdminPanelController(mockedHttpContext.Object, mockedProvider.Object, mockedMapper.Object,
                mockedUsersService.Object, mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            // Act
            var computer = new Computer();
            var laptop = new Laptop();
            var display = new Display();
            var user = new User();

            var computersCollection = new List<Computer>() { computer };
            var laptopsCollection = new List<Laptop>() { laptop };
            var displaysCollection = new List<Display>() { display };
            var userssCollection = new List<User>() { user };


            mockedComputersService.Setup(c => c.GetAll()).Returns(computersCollection.AsQueryable());

            mockedLaptopsService.Setup(c => c.GetAll()).Returns(laptopsCollection.AsQueryable());

            mockedDisplaysService.Setup(c => c.GetAll()).Returns(displaysCollection.AsQueryable());

            mockedUsersService.Setup(c => c.GetAll()).Returns
                (userssCollection.AsQueryable());

            //Assert
            controller
                .WithCallTo(c => c.Index("Display", display.Id))
                .ShouldReturnJson();
        }

        [Test]
        public void Index_ShouldReturnsTrue_WhenUsers_AreValid()
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

                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<UserViewModel, User>();
            });

            var mockedHttpContext = new Mock<IHttpContextProvider>();
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new AdminPanelController(mockedHttpContext.Object, mockedProvider.Object, mockedMapper.Object,
                mockedUsersService.Object, mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            // Act
            var computer = new Computer();
            var laptop = new Laptop();
            var display = new Display();
            var user = new User();

            var computersCollection = new List<Computer>() { computer };
            var laptopsCollection = new List<Laptop>() { laptop };
            var displaysCollection = new List<Display>() { display };
            var userssCollection = new List<User>() { user };


            mockedComputersService.Setup(c => c.GetAll()).Returns(computersCollection.AsQueryable());

            mockedLaptopsService.Setup(c => c.GetAll()).Returns(laptopsCollection.AsQueryable());

            mockedDisplaysService.Setup(c => c.GetAll()).Returns(displaysCollection.AsQueryable());

            mockedUsersService.Setup(c => c.GetAll()).Returns
                (userssCollection.AsQueryable());

            //Assert
            controller
                .WithCallTo(c => c.Index("User", new Guid(user.Id)))
                .ShouldReturnJson();
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

            var mockedHttpContext = new Mock<IHttpContextProvider>();
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new AdminPanelController(mockedHttpContext.Object, mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            //Act and Assert
            controller
                .WithCallTo(c => c.Computers())
                .ShouldRenderPartialView("Computers");
        }

        [Test]
        public void AddComputerGET_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new AdminPanelController();

            // Act and Assert
            controller
                .WithCallTo(c => c.AddComputer())
                .ShouldRenderPartialView("AddComputer");
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

            var mockedHttpContext = new Mock<IHttpContextProvider>();
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new AdminPanelController(mockedHttpContext.Object, mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
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
                .ShouldRedirectTo(c => c.Index());
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

            var mockedHttpContext = new Mock<IHttpContextProvider>();
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new AdminPanelController(mockedHttpContext.Object, mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            //Act and Assert
            controller
                .WithCallTo(c => c.Laptops())
                .ShouldRenderPartialView("Laptops");
        }

        [Test]
        public void AddLaptopGET_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new AdminPanelController();

            // Act and Assert
            controller
                .WithCallTo(c => c.AddLaptop())
                .ShouldRenderPartialView("AddLaptop");
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

            var mockedHttpContext = new Mock<IHttpContextProvider>();
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new AdminPanelController(mockedHttpContext.Object, mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
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
                .ShouldRedirectTo(c => c.Index());
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

            var mockedHttpContext = new Mock<IHttpContextProvider>();
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new AdminPanelController(mockedHttpContext.Object, mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
            mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

            //Act and Assert
            controller
                .WithCallTo(c => c.Displays())
                .ShouldRenderPartialView("Displays");
        }

        [Test]
        public void AddDisplayGET_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new AdminPanelController();

            // Act and Assert
            controller
                .WithCallTo(c => c.AddDisplay())
                .ShouldRenderPartialView("AddDisplay");
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

            var mockedHttpContext = new Mock<IHttpContextProvider>();
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedComputersService = new Mock<IComputersService>();
            var mockedLaptopsService = new Mock<ILaptopsService>();
            var mockedDisplaysService = new Mock<IDisplaysService>();

            var controller = new AdminPanelController(mockedHttpContext.Object, mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
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
                .ShouldRedirectTo(c => c.Index());
        }
    }
}
