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
    public class CartControllerTests
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
            var target = new CartController(mock.Object, null);

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
            var target = new CartController(mock.Object, null);

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
            var target = new CartController(null, null);

            // Act - add a product to the cart
            var result = target.Index(cart, "myUrl").ViewData.Model as CartIndexViewModel;

            // Assert
            Assert.AreSame(cart, result.Cart);
            Assert.AreEqual("myUrl", result.ReturnUrl);
        }


        [TestMethod]
        public void Cannot_Checkout_Empty_Cart()
        {
            // Arrange - create a mock order processor
            var mock = new Mock<IOrderProcessor>();

            // Arrange - create an empty cart
            var cart = new Cart();

            // Arrange - create shipping details
            var shippingDetails = new ShippingDetails();

            // Arrange - create and instance of the controller
            var target = new CartController(null, mock.Object);

            // Act
            var result = target.Checkout(cart, shippingDetails);

            // Assert - check that the order hasn't been passed on to the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());

            // Assert - check that the method is returning the default view
            Assert.AreEqual("", result.ViewName);

            // Assert - check that we are passing an invalid model to the view
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            // Arrange - create a mock order processor
            var mock = new Mock<IOrderProcessor>();

            // Arrange - create a cart with an item
            var cart = new Cart();
            cart.AddItem(new Product(), 1);

            // Arrange - create and instance of the controller
            var target = new CartController(null, mock.Object);

            // Arrange - add an error to the model
            target.ModelState.AddModelError("error", "error");

            // Act - try to checkout
            var result = target.Checkout(cart, new ShippingDetails());

            // Assert - check that the order hasn't been passed on to the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());

            // Assert - check that the method is returning the default view
            Assert.AreEqual("", result.ViewName);

            // Assert - check that we are passing the model passed to the view is of type Shipping Details
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(ShippingDetails));

            // Assert - check that we are passing an invalid model to the view
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Can_Checkout_And_Submit_Order()
        {
            // Arrange - create a mock order processor
            var mock = new Mock<IOrderProcessor>();

            // Arrange - create a cart with an item
            var cart = new Cart();
            cart.AddItem(new Product(), 1);

            // Arrange - create and instance of the controller
            var target = new CartController(null, mock.Object);

            // Act - try to checkout
            var result = target.Checkout(cart, new ShippingDetails());

            // Assert - check that the order has been passed on to the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once());

            // Assert - check that the cart has been empty
            Assert.AreEqual(0, cart.Lines.Count());

            // Assert - check that the method is returning the Completed view
            Assert.AreEqual("Completed", result.ViewName);

            // Assert - check that we are passing an valid model to the view
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
        }
    }
}