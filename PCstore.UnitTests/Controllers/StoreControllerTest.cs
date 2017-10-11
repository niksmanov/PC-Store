using System.Web.Mvc;
using PCstore.Web.Controllers;
using NUnit.Framework;

namespace PCstore.UnitTests.Controllers
{
    [TestFixture]
    public class StoreControllerTest
    {
        [Test]
        public void Index_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new StoreController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("PC store - Home", result.ViewData["Title"]);
        }

        [Test]
        public void About_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new StoreController();

            // Act
            var result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("About", result.ViewData["Title"]);
        }

        [Test]
        public void Contact_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new StoreController();

            // Act
            var result = controller.Contact() as ViewResult;

            // Assert
            Assert.AreEqual("Contact", result.ViewData["Title"]);
        }
    }
}
