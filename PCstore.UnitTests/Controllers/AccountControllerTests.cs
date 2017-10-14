using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Moq;
using NUnit.Framework;
using PCstore.Data.Model;
using PCstore.Web.Controllers;
using PCstore.Web.Models;
using PCstore.Web.Providers.Contracts;
using System;
using TestStack.FluentMVCTesting;

namespace PCstore.UnitTests.Controllers
{
    [TestFixture]
    public class AccountControllerTests
    {
        [Test]
        public void Controller_ShouldNotThrowArgumentNullException_WhenPassedParametersAreNull()
        {
            // Arrange
            var mockedVerification = new Mock<IVerificationProvider>();

            // Act and Assert
            Assert.DoesNotThrow(() => new AccountController(mockedVerification.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenPassedParametersAreNull()
        {
            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => new AccountController(null));
        }

        [Test]
        public void LoginGET_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var returnUrl = "returnUrl";
            var mockedVerification = new Mock<IVerificationProvider>();
            var controller = new AccountController(mockedVerification.Object);

            // Act
            mockedVerification.Setup(x => x.IsAuthenticated).Returns(false);

            // Assert
            controller
                .WithCallTo(c => c.Login(returnUrl))
                .ShouldRenderView("Login");
        }

        [Test]
        public void LoginGET_ShouldRedirects_WhenLoggedIn()
        {
            // Arrange
            var returnUrl = "returnUrl";
            var mockedVerification = new Mock<IVerificationProvider>();
            var controller = new AccountController(mockedVerification.Object);

            // Act
            mockedVerification.Setup(x => x.IsAuthenticated).Returns(true);

            // Assert
            controller
                .WithCallTo(c => c.Login(returnUrl))
                .ShouldRedirectTo((StoreController c) => c.Index());
        }


        [Test]
        public void LoginPOST_ShouldRedirects_WhenLoggedIn()
        {
            // Arrange
            var returnUrl = "returnUrl";
            var mockedVerification = new Mock<IVerificationProvider>();
            var controller = new AccountController(mockedVerification.Object);
            var viewModel = new LoginViewModel();

            // Act
            mockedVerification.Setup(x => x.IsAuthenticated).Returns(true);

            // Assert
            controller
                .WithCallTo(c => c.Login(viewModel, returnUrl))
                .ShouldRedirectTo((StoreController c) => c.Index());
        }

        [Test]
        public void LoginPOST_ShouldRenderView_WhenIsNotValid()
        {
            // Arrange
            var returnUrl = "returnUrl";
            var mockedVerification = new Mock<IVerificationProvider>();
            var controller = new AccountController(mockedVerification.Object);
            controller.ModelState.AddModelError("wrorng model", "Error");
            var viewModel = new LoginViewModel();

            // Act and Assert
            controller
                .WithCallTo(c => c.Login(viewModel, returnUrl))
                .ShouldRenderDefaultView();
        }

        [Test]
        public void LoginPOST_ShouldLoggedIn_CurrentUser()
        {
            // Arrange
            var returnUrl = "returnUrl";
            var mockedVerification = new Mock<IVerificationProvider>();

            var viewModel = new LoginViewModel()
            {
                Password = "password",
                Email = "pesho@abv.bg",
                RememberMe = true
            };

            mockedVerification.Setup(x => x.GetUserByEmail(viewModel.Email));

            mockedVerification.Setup(v => v.SignInWithPassword(viewModel.Email, viewModel.Password, viewModel.RememberMe, It.IsAny<bool>()))
                .Returns(SignInStatus.Success);

            var controller = new AccountController(mockedVerification.Object);


            // Act and Assert
            controller
                .WithCallTo(c => c.Login(viewModel, returnUrl))
                .ShouldRedirectTo(returnUrl);
        }

        [Test]
        public void LoginPOST_ShouldThrowError_IfCurrentUserIsNotValid()
        {
            // Arrange
            var returnUrl = "returnUrl";
            var mockedVerification = new Mock<IVerificationProvider>();

            var viewModel = new LoginViewModel()
            {
                Password = "password",
                Email = "pesho@abv.bg",
                RememberMe = true
            };

            mockedVerification.Setup(x => x.GetUserByEmail(viewModel.Email));


            mockedVerification.Setup(v => v.SignInWithPassword(viewModel.Email, viewModel.Password, viewModel.RememberMe, It.IsAny<bool>()))
                .Returns(SignInStatus.Failure);

            var controller = new AccountController(mockedVerification.Object);


            // Act and Assert
            controller
                .WithCallTo(c => c.Login(viewModel, returnUrl))
                .ShouldRenderDefaultView();
        }

        [Test]
        public void LoginPOST_ShouldThrowError_IfCurrentUserIsBlocked()
        {
            // Arrange
            var returnUrl = "returnUrl";
            var mockedVerification = new Mock<IVerificationProvider>();

            var blockedUser = new User
            {
                Email = "pesho@abv.bg",
                DeletedOn = DateTime.Now
            };

            var viewModel = new LoginViewModel()
            {
                Password = "password",
                Email = "pesho@abv.bg",
                RememberMe = true
            };

            mockedVerification.Setup(x => x.GetUserByEmail(blockedUser.Email)).Returns(blockedUser);


            mockedVerification.Setup(v => v.SignInWithPassword(viewModel.Email, viewModel.Password, viewModel.RememberMe, It.IsAny<bool>()))
                .Returns(SignInStatus.Failure);

            var controller = new AccountController(mockedVerification.Object);


            // Act and Assert
            controller
                .WithCallTo(c => c.Login(viewModel, returnUrl))
                .ShouldRenderDefaultView();
        }

        [Test]
        public void RegisterGET_ShouldRedirects_WhenRegistered()
        {
            // Arrange
            var mockedVerification = new Mock<IVerificationProvider>();
            var controller = new AccountController(mockedVerification.Object);

            // Act
            mockedVerification.Setup(x => x.IsAuthenticated).Returns(true);

            // Assert
            controller
                .WithCallTo(c => c.Register())
                .ShouldRedirectTo((StoreController c) => c.Index());
        }

        [Test]
        public void RegisterGET_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var mockedVerification = new Mock<IVerificationProvider>();
            var controller = new AccountController(mockedVerification.Object);

            // Act
            mockedVerification.Setup(x => x.IsAuthenticated).Returns(false);

            // Assert
            controller
                .WithCallTo(c => c.Register())
                .ShouldRenderView("Register");
        }

        [Test]
        public void RegisterPOST_ShouldRedirects_WhenRegistered()
        {
            // Arrange
            var mockedVerification = new Mock<IVerificationProvider>();
            var controller = new AccountController(mockedVerification.Object);
            var viewModel = new RegisterViewModel();

            // Act
            mockedVerification.Setup(x => x.IsAuthenticated).Returns(true);

            // Assert
            controller
                .WithCallTo(c => c.Register(viewModel))
                .ShouldRedirectTo((StoreController c) => c.Index());
        }

        [Test]
        public void RegisterPOST_ShouldRegister_NewUser()
        {
            // Arrange
            var mockedVerification = new Mock<IVerificationProvider>();
            var mockedUser = new Mock<User>();

            mockedVerification.Setup(v => v.RegisterAndLoginUser(It.IsAny<User>(), It.IsAny<string>(),
                It.IsAny<bool>(), It.IsAny<bool>())).Returns(IdentityResult.Success);

            var controller = new AccountController(mockedVerification.Object);

            var viewModel = new RegisterViewModel()
            {
                Password = "password",
                Email = "pesho@abv.bg"
            };

            controller.Register(viewModel);

            // Act and Assert
            controller
                .WithCallTo(c => c.Register(viewModel))
                .ShouldRedirectTo((StoreController c) => c.Index());
        }

        [Test]
        public void LogOff_ShouldRedirects_WhenSuccessfulLogOff()
        {
            // Arrange
            var mockedVerification = new Mock<IVerificationProvider>();
            var controller = new AccountController(mockedVerification.Object);

            // Act
            mockedVerification.Setup(x => x.IsAuthenticated).Returns(false);

            // Assert
            controller
                .WithCallTo(c => c.LogOff())
                .ShouldRedirectTo((StoreController c) => c.Index());
        }

        [Test]
        public void LogOff_ShouldRedirects_WhenNotSuccessfulLogOff()
        {
            // Arrange
            var mockedVerification = new Mock<IVerificationProvider>();
            var controller = new AccountController(mockedVerification.Object);

            // Act
            mockedVerification.Setup(x => x.IsAuthenticated).Returns(true);
            mockedVerification.Setup(x => x.SignOut());

            // Assert
            controller
                .WithCallTo(c => c.LogOff())
                .ShouldRedirectTo((StoreController c) => c.Index());
        }
    }
}
