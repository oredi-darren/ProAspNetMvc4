using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools.Models;
using Moq;

namespace EssentialTools.Tests
{
    [TestClass]
    public class UnitTest2
    {
        private Product[] _products = {
                new Product { Name = "Kayak", Price = 275M, Category = "Watersports" },
                new Product { Name = "Lifejacket", Price = 48.95M, Category = "Watersports" },
                new Product { Name = "Soccer ball", Price = 19.50M, Category = "Soccer" },
                new Product { Name = "Corner flag", Price = 34.95M, Category = "Soccer" }
            };

        [TestMethod]
        public void Sum_Products_Correctly()
        {
            // arrange
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>()))
                .Returns<decimal>(total => total);
            var target = new LinqValueCalculator(mock.Object);
            var goalTotal = _products.Sum(e => e.Price);

            // act
            var result = target.ValueProducts(_products);

            // assert
            Assert.AreEqual(goalTotal, result);
        }

        private Product[] createProduct(decimal value) {
            return new[] { new Product { Price = value } };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Pass_Through_VariableDiscounts()
        {
            // arrange
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>()))
                .Returns<decimal>(total => total);
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v < 0)))
                .Throws<ArgumentOutOfRangeException>();
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v > 100)))
                .Returns<decimal>(total => total * 0.9M);
            mock.Setup(m => m.ApplyDiscount(It.IsInRange<decimal>(10, 100, Range.Inclusive)))
                .Returns<decimal>(total => total - 5);
            var target = new LinqValueCalculator(mock.Object);

            // act
            var FiveDollarDiscount = target.ValueProducts(createProduct(5));
            var TenDollarDiscount = target.ValueProducts(createProduct(10));
            var FiftyDollarDiscount = target.ValueProducts(createProduct(50));
            var HundredDollarDiscount = target.ValueProducts(createProduct(100));
            var FiveHundredDollarDiscount = target.ValueProducts(createProduct(500));
            
            // assert
            Assert.AreEqual(5, FiveDollarDiscount, "$5 Fail");
            Assert.AreEqual(5, TenDollarDiscount, "$10 Fail");
            Assert.AreEqual(45, FiftyDollarDiscount, "$50 Fail");
            Assert.AreEqual(95, HundredDollarDiscount, "$100 Fail");
            Assert.AreEqual(450, FiveHundredDollarDiscount, "$500 Fail");
            target.ValueProducts(createProduct(-1));
        }
    }
}
