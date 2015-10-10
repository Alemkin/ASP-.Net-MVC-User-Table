using FirstDemoProjectForRPbyAL.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace FirstDemoProjectForRPbyAL.Tests.Controllers
{
    /**
     * This class Tests the functionality of 
     * the methods in the ErrorController class
     * 
     * <author> Alexander Lemkin </author>
     */
    [TestClass]
    public class ErrorControllerTest
    {
        [TestMethod]
        public void Index()
        {
            var controller = new ErrorController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BadRequest()
        {
            var controller = new ErrorController();

            // Act
            var result = controller.BadRequest() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void InternalServerError()
        {
            var controller = new ErrorController();

            // Act
            var result = controller.InternalServerError() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void NotFound()
        {
            var controller = new ErrorController();

            // Act
            var result = controller.NotFound() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
