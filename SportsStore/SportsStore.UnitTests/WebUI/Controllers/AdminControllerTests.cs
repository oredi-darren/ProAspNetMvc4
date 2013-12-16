using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.UnitTests.WebUI.Controllers
{
    [TestClass]
    public class AdminControllerTests
    {
        [TestMethod]
        public void Can_Delete_Valid_Products()
        {
            // Arrange - create a product
            var product = new Product { ProductID = 2, Name = "Test" };

            // Arrange - create the mock repository
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] { 
                new Product { ProductID = 1, Name = "P1" },
                product, 
                new Product { ProductID = 3, Name = "P3" }
            }.AsQueryable());

            // Arrange - create a controller
            var target = new AdminController(mock.Object);

            // Act - delete the product
            target.Delete(product.ProductID);

            // Asert - check that the repository was called
            mock.Verify(m => m.DeleteProduct(product.ProductID));
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Arrange - create the mock repository
            var mock = new Mock<IProductRepository>();

            // Arrange - create a controller
            var target = new AdminController(mock.Object);

            // Arrange - create a product
            var product = new Product { Name = "Test" };

            // Action
            var result = target.Edit(product, null);

            // Asert - check that the repository was called
            mock.Verify(m => m.SaveProduct(product));

            // Asert - check the method result type
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Arrange - create the mock repository
            var mock = new Mock<IProductRepository>();

            // Arrange - create a controller
            var target = new AdminController(mock.Object);

            // Arrange - create a product
            var product = new Product { Name = "Test" };

            // Arrange - add an error to the model state
            target.ModelState.AddModelError("error", "error");
            
            // Action
            var result = target.Edit(product, null);

            // Asert - check that the repository was called
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());

            // Asert - check the method result type
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Index_Contains_All_Products()
        {
            // Arrange - create the mock repository
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product { ProductID = 1, Name = "P1", Category = "Apples" },
                new Product { ProductID = 2, Name = "P2", Category = "Apples" },
                new Product { ProductID = 3, Name = "P3", Category = "Apples" }
            }.AsQueryable());

            // Arrange - create a controller
            var target = new AdminController(mock.Object);

            // Action
            var result = (target.Index().ViewData.Model as IEnumerable<Product>).ToArray();

            // Asert
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);
        }

        [TestMethod]
        public void Can_Edit_Product()
        {
            // Arrange - create the mock repository
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product { ProductID = 1, Name = "P1", Category = "Apples" },
                new Product { ProductID = 2, Name = "P2", Category = "Apples" },
                new Product { ProductID = 3, Name = "P3", Category = "Apples" }
            }.AsQueryable());

            // Arrange - create a controller
            var target = new AdminController(mock.Object);

            // Action
            var p1 = target.Edit(1).ViewData.Model as Product;
            var p2 = target.Edit(2).ViewData.Model as Product;
            var p3 = target.Edit(3).ViewData.Model as Product;

            // Asert
            Assert.AreEqual(1, p1.ProductID);
            Assert.AreEqual(2, p2.ProductID);
            Assert.AreEqual(3, p3.ProductID);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Product()
        {
            // Arrange - create the mock repository
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product { ProductID = 1, Name = "P1", Category = "Apples" },
                new Product { ProductID = 2, Name = "P2", Category = "Apples" },
                new Product { ProductID = 3, Name = "P3", Category = "Apples" }
            }.AsQueryable());

            // Arrange - create a controller
            var target = new AdminController(mock.Object);

            // Action
            var result = target.Edit(4).ViewData.Model as Product;

            // Asert
            Assert.IsNull(result);
        }
    }
}
