using System.Web.Mvc;
using PCstore.Web.Controllers;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

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

            // Act and Assert
            controller
                .WithCallTo(c => c.Index())
                .ShouldRenderView("Index");
        }

        [Test]
        public void About_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new StoreController();

            // Act and Assert
            controller
                .WithCallTo(c => c.About())
                .ShouldRenderView("About");
        }

        [Test]
        public void Contact_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new StoreController();

            // Act
            var result = controller.Contact() as ViewResult;

            // Act and Assert
            controller
                .WithCallTo(c => c.Contact())
                .ShouldRenderView("Contact");
        }
    }
}
