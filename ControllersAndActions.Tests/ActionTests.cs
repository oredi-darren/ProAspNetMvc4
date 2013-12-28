using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControllersAndActions.Controllers;
using System.Web.Mvc;

namespace ControllersAndActions.Tests
{
    [TestClass]
    public class ActionTests
    {
        [TestMethod]
        public void ViewSelectionTest()
        {
            // Arrange - create the controller
            var target = new ExampleController();

            // Act - call the action method
            var result = target.Index() as ViewResult;

            // Assert - check the result
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual("Hello", result.ViewBag.Message);
        }

        [TestMethod]
        public void RedirectValueTest()
        {
            // Arrange - create the controller
            var target = new ExampleController();

            // Act - call the action method
            var result = target.Redirect() as RedirectToRouteResult;

            // Assert - check the result
            Assert.IsFalse(result.Permanent);
            Assert.AreEqual("Example", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("MyID", result.RouteValues["ID"]);
        }


        [TestMethod]
        public void StatusCodeResultTest()
        {
            // Arrange - create the controller
            var target = new ExampleController();

            // Act - call the action method
            var result = target.StatusCode() as HttpStatusCodeResult;

            // Assert - check the result
            Assert.AreEqual(404, result.StatusCode);
        }
    }
}
