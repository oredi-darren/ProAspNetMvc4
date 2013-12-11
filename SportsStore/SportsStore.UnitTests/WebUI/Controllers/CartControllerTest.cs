using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests.WebUI.Controllers
{
    [TestClass]
    public class CartControllerTest
    {
        [TestMethod]
        public void Can_Add_To_Cart()
        {
            // Arrange - create the mock repository
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product { ProductID = 1, Name = "P1", Category = "Apples" }
            }.AsQueryable());

            // Arrange - create a cart
            var cart = new Cart();

            // Arrange - create the controller
            var target = new CartController(mock.Object);

            // Act - add a product to the cart
            target.AddToCart(cart, 1, null);

            // Assert
            Assert.AreEqual(1, cart.Lines.Count());
            Assert.AreEqual(1, cart.Lines.ToArray()[0].Product.ProductID);
        }

        [TestMethod]
        public void Adding_Product_To_Cart_Goes_To_Cart_Screen()
        {
            // Arrange - create the mock repository
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product { ProductID = 1, Name = "P1", Category = "Apples" }
            }.AsQueryable());

            // Arrange - create a cart
            var cart = new Cart();

            // Arrange - create the controller
            var target = new CartController(mock.Object);

            // Act - add a product to the cart
            var result = target.AddToCart(cart, 1, "myUrl");

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("myUrl", result.RouteValues["returnUrl"]);
        }


        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            // Arrange - create a cart
            var cart = new Cart();

            // Arrange - create the controller
            var target = new CartController(null);

            // Act - add a product to the cart
            var result = target.Index(cart, "myUrl").ViewData.Model as CartIndexViewModel;

            // Assert
            Assert.AreSame(cart, result.Cart);
            Assert.AreEqual("myUrl", result.ReturnUrl);
        }
    }
}