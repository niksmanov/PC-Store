using Moq;
using NUnit.Framework;
using PCstore.Web.Controllers;
using PCstore.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace PCstore.UnitTests.Controllers
{
    [TestFixture]
    public class AccountControllerTests
    {
        [Test]
        public void Login_ShouldStoreTheReturnUrlInViewBag()
        {
            // Arrange
            var returnUrl = "returnUrl";
            var controller = new AccountController();

            // Act
            var result = controller.Login(returnUrl) as ViewResult;

            // Assert
            Assert.AreEqual(returnUrl, result.ViewBag.ReturnUrl);
        }

        [Test]
        public void Login_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var returnUrl = "returnUrl";
            var controller = new AccountController();

            // Act
            var result = controller.Login(returnUrl) as ViewResult;

            // Assert
            Assert.AreEqual("Log in", result.ViewData["Title"]);
        }

        [Test]
        public void Login_ShouldReturnsTrue_IfHaveAntiForgeryAttrubute()
        {
            // Arrange
            var loginMethod = typeof(AccountController)
              .GetMethods()
              .FirstOrDefault(x => x.Name == "Login" &&
              x.GetParameters().Count() == 2);

            // Act
            var attributes = loginMethod.GetCustomAttributes(false)
                 .Last().GetType().Name;

            // Assert
            Assert.AreEqual("ValidateAntiForgeryTokenAttribute", attributes);
        }

        [Test]
        public void Register_ShouldReturnsTrue_IfHaveAntiForgeryAttrubute()
        {
            // Arrange
            var registerMethod = typeof(AccountController)
              .GetMethods()
              .FirstOrDefault(x => x.Name == "Register" &&
              x.GetParameters().Count() == 1);

            // Act
            var attributes = registerMethod.GetCustomAttributes(false)
                    .Last().GetType().Name;

            // Assert
            Assert.AreEqual("ValidateAntiForgeryTokenAttribute", attributes);
        }


        [Test]
        public void Register_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.Register() as ViewResult;

            // Assert
            Assert.AreEqual("Register", result.ViewData["Title"]);
        }

        [Test]
        public void VerifyCodeGET_ShouldReturnsTrue_IfHaveAllowAnonymousAttribute()
        {
            // Arrange
            var verifyCodeMethod = typeof(AccountController)
              .GetMethods()
              .FirstOrDefault(x => x.Name == "VerifyCode" &&
              x.GetParameters().Count() == 3);

            // Act
            var attributes = verifyCodeMethod.GetCustomAttributes(false)
                  .Last().GetType().Name;

            // Assert
            Assert.AreEqual("AllowAnonymousAttribute", attributes);
        }


        [Test]
        public void VerifyCodePOST_ShouldReturnsTrue_IfHaveAntiForgeryAttrubute()
        {
            // Arrange
            var verifyCodeMethod = typeof(AccountController)
              .GetMethods()
              .FirstOrDefault(x => x.Name == "VerifyCode" &&
              x.GetParameters().Count() == 1);

            // Act
            var attributes = verifyCodeMethod.GetCustomAttributes(false)
                  .Last().GetType().Name;

            // Assert
            Assert.AreEqual("ValidateAntiForgeryTokenAttribute", attributes);
        }

        [Test]
        public void ConfirmEmail_ShouldReturnsTrue_IfHaveAllowAnonymousAttribute()
        {
            // Arrange
            var confirmEmailMethod = typeof(AccountController)
              .GetMethods()
              .FirstOrDefault(x => x.Name == "ConfirmEmail" &&
              x.GetParameters().Count() == 2);

            // Act
            var attributes = confirmEmailMethod.GetCustomAttributes(false)
                  .Last().GetType().Name;

            // Assert
            Assert.AreEqual("AllowAnonymousAttribute", attributes);
        }

        [Test]
        public void ForgotPassword_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ForgotPassword() as ViewResult;

            // Assert
            Assert.AreEqual("Forgot your password?", result.ViewData["Title"]);
        }

        [Test]
        public void ForgotPassword_ShouldReturnsTrue_IfHaveAntiForgeryAttrubute()
        {
            // Arrange
            var forgotPasswordMethod = typeof(AccountController)
              .GetMethods()
              .FirstOrDefault(x => x.Name == "ForgotPassword" &&
              x.GetParameters().Count() == 1);

            // Act
            var attributes = forgotPasswordMethod.GetCustomAttributes(false)
                .Last().GetType().Name;

            // Assert
            Assert.AreEqual("ValidateAntiForgeryTokenAttribute", attributes);
        }

        [Test]
        public void ForgotPasswordConfirmation_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ForgotPasswordConfirmation() as ViewResult;

            // Assert
            Assert.AreEqual("Forgot Password Confirmation", result.ViewData["Title"]);
        }

        [Test]
        public void ResetPassword_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ResetPassword(string.Empty) as ViewResult;

            // Assert
            Assert.AreEqual("Reset password", result.ViewData["Title"]);
        }

        [Test]
        public void ResetPassword_ShouldReturnsTrue_WhenResetCode_IsValid()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ResetPassword(string.Empty) as ViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [Test]
        public void ResetPassword_ShouldReturnsTrue_IfHaveAntiForgeryAttrubute()
        {
            // Arrange
            var resetPasswordMethod = typeof(AccountController)
              .GetMethods()
              .Where(x => x.Name == "ResetPassword" &&
              x.GetParameters().Count() == 1).Last();

            // Act
            var attributes = resetPasswordMethod.GetCustomAttributes(false)
                   .Last().GetType().Name;

            // Assert
            Assert.AreEqual("ValidateAntiForgeryTokenAttribute", attributes);
        }

        [Test]
        public void ResetPasswordConfirmation_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ResetPasswordConfirmation() as ViewResult;

            // Assert
            Assert.AreEqual("Reset password confirmation", result.ViewData["Title"]);
        }


        [Test]
        public void ExternalLogin_ShouldReturnsTrue_IfHaveAntiForgeryAttrubute()
        {
            // Arrange
            var externalLoginMethod = typeof(AccountController)
              .GetMethods()
              .FirstOrDefault(x => x.Name == "ExternalLogin" &&
              x.GetParameters().Count() == 2);

            // Act
            var attributes = externalLoginMethod.GetCustomAttributes(false)
                 .Last().GetType().Name;

            // Assert
            Assert.AreEqual("ValidateAntiForgeryTokenAttribute", attributes);
        }

        [Test]
        public void SendCodeGET_ShouldReturnsTrue_IfHaveAllowAnonymousAttribute()
        {
            // Arrange
            var sendCodeMethod = typeof(AccountController)
              .GetMethods()
              .FirstOrDefault(x => x.Name == "SendCode" &&
              x.GetParameters().Count() == 2);

            // Act
            var attributes = sendCodeMethod.GetCustomAttributes(false)
                 .Last().GetType().Name;

            // Assert
            Assert.AreEqual("AllowAnonymousAttribute", attributes);
        }

        [Test]
        public void SendCodePOST_ShouldReturnsTrue_IfHaveAntiForgeryAttrubute()
        {
            // Arrange
            var sendCodeMethod = typeof(AccountController)
              .GetMethods()
              .FirstOrDefault(x => x.Name == "SendCode" &&
              x.GetParameters().Count() == 1);

            // Act
            var attributes = sendCodeMethod.GetCustomAttributes(false)
                 .Last().GetType().Name;

            // Assert
            Assert.AreEqual("ValidateAntiForgeryTokenAttribute", attributes);
        }

        [Test]
        public void ExternalLoginCallback_ShouldReturnsTrue_IfHaveAllowAnonymousAttribute()
        {
            // Arrange
            var externalLoginCallbackMethod = typeof(AccountController)
              .GetMethods()
              .FirstOrDefault(x => x.Name == "ExternalLoginCallback" &&
              x.GetParameters().Count() == 1);

            // Act
            var attributes = externalLoginCallbackMethod.GetCustomAttributes(false)
                 .Last().GetType().Name;

            // Assert
            Assert.AreEqual("AllowAnonymousAttribute", attributes);
        }

        [Test]
        public void ExternalLoginConfirmation_ShouldReturnsTrue_IfHaveAntiForgeryAttrubute()
        {
            // Arrange
            var externalLoginConfirmationMethod = typeof(AccountController)
              .GetMethods()
              .SingleOrDefault(x => x.Name == "ExternalLoginConfirmation" &&
              x.GetParameters().Count() == 2);

            // Act
            var attributes = externalLoginConfirmationMethod.GetCustomAttributes(false)
                .Last().GetType().Name;


            // Assert
            Assert.AreEqual("ValidateAntiForgeryTokenAttribute", attributes);
        }

        [Test]
        public void ExternalLoginFailure_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ExternalLoginFailure() as ViewResult;

            // Assert
            Assert.AreEqual("Login Failure", result.ViewData["Title"]);
        }
    }
}
