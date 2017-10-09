using System.Web.Mvc;
using PCstore.Web.Controllers;
using NUnit.Framework;

namespace PCstore.UnitTests.Controllers
{
    [TestFixture]
    public class StoreControllerTest
    {
        [Test]
        public void ShouldReturnsTrue_WhenIndexViewResult_IsValid()
        {
            // Arrange
            StoreController controller = new StoreController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("PC store - Home", result.ViewData["Title"]);
        }

        [Test]
        public void ShouldReturnsTrue_WhenAboutViewResult_IsValid()
        {
            // Arrange
            StoreController controller = new StoreController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("About", result.ViewData["Title"]);
        }

        [Test]
        public void ShouldReturnsTrue_WhenContactViewResult_IsValid()
        {
            // Arrange
            StoreController controller = new StoreController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.AreEqual("Contact", result.ViewData["Title"]);
        }
    }
}
