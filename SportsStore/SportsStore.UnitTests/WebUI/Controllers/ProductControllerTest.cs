using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SportsStore.WebUI.HtmlHelpers;

namespace SportsStore.UnitTests.WebUI.Controllers
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        public void Can_Paginate()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
                    new Product { ProductId = 1, Name = "P1" },
                    new Product { ProductId = 2, Name = "P2" },
                    new Product { ProductId = 3, Name = "P3" },
                    new Product { ProductId = 4, Name = "P4" },
                    new Product { ProductId = 5, Name = "P5" }
                }.AsQueryable());

            var controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            var result = controller.List(2).Model as ProductListViewModel;
            var array = result.Products.ToArray();
            Assert.IsTrue(array.Length == 2);
            Assert.AreEqual(array[0].Name, "P4");
            Assert.AreEqual(array[1].Name, "P5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            // Arrange - define an HTML helper - we need to do this
            // in order to apply the extension method
            var helper = default(HtmlHelper);

            // Arrange - create the PagingInfo data
            var pagingInfo = new PagingInfo { 
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Arrange - set up the delegate using a lambda expression
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Act
            var result = helper.PageLinks(pagingInfo, pageUrlDelegate);

            // Assert
            Assert.AreEqual(result.ToString(), @"<a href=""Page1"">1</a>"
                + @"<a class=""selected"" href=""Page2"">2</a>"
                + @"<a href=""Page3"">3</a>");
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
                    new Product { ProductId = 1, Name = "P1" },
                    new Product { ProductId = 2, Name = "P2" },
                    new Product { ProductId = 3, Name = "P3" },
                    new Product { ProductId = 4, Name = "P4" },
                    new Product { ProductId = 5, Name = "P5" }
                }.AsQueryable());

            // Arrange
            var controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Act
            var result = controller.List(2).Model as ProductListViewModel;

            // Assert
            var pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }
    }
}
