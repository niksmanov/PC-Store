using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCstore.Web.Controllers;

namespace PCstore.UnitTests.Controllers
{
    [TestClass]
    public class StoreControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            StoreController controller = new StoreController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("PC store - Home", result.ViewData["Title"]);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            StoreController controller = new StoreController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("About", result.ViewData["Title"]);
        }

        [TestMethod]
        public void Contact()
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
