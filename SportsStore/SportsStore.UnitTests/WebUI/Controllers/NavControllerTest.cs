using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Abstract;
using Moq;
using SportsStore.Domain.Entities;
using System.Collections.Generic;
using SportsStore.WebUI.Controllers;

namespace SportsStore.UnitTests.WebUI.Controllers
{
    [TestClass]
    public class NavControllerTest
    {
        [TestMethod]
        public void Can_Create_Categories()
        {
            // Arrange
            // - create the mock repository
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
                    new Product { ProductID = 1, Name = "P1", Category = "Apples" },
                    new Product { ProductID = 2, Name = "P2", Category = "Apples" },
                    new Product { ProductID = 3, Name = "P3", Category = "Plums" },
                    new Product { ProductID = 4, Name = "P4", Category = "Oranges" }
                }.AsQueryable());

            // Arrange - create a controller
            var target = new NavController(mock.Object);

            // Action
            var result = (target.Menu().Model as IEnumerable<string>).ToArray();

            // Assert
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("Apples", result[0]);
            Assert.AreEqual("Oranges", result[1]);
            Assert.AreEqual("Plums", result[2]);
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            // Arrange
            // - create the mock repository
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
                    new Product { ProductID = 1, Name = "P1", Category = "Apples" },
                    new Product { ProductID = 2, Name = "P2", Category = "Apples" },
                    new Product { ProductID = 3, Name = "P3", Category = "Plums" },
                    new Product { ProductID = 4, Name = "P4", Category = "Oranges" }
                }.AsQueryable());

            // Arrange - create a controller
            var target = new NavController(mock.Object);

            // Arrange - define the category to select
            string categoryToSelect = "Apples";

            // Action
            var result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            // Assert
            Assert.AreEqual(categoryToSelect, result);
        }
    }
}
