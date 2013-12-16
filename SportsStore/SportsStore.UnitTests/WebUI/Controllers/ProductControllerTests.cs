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
    public class ProductControllerTests
    {
        [TestMethod]
        public void Cannnot_Retrieve_Image_Data_For_Invalid_ID()
        {
            // Arrange - create the mock repository
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] { 
                new Product { ProductID = 1, Name = "P1" },
                new Product { ProductID = 2, Name = "P2" }
            }.AsQueryable());

            // Arrange - create a controller
            var target = new ProductController(mock.Object);

            // Act - call the GetImage action method
            var result = target.GetImage(100);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            // Arrange - create a product with image data
            var product = new Product { ProductID = 2, Name = "Test", ImageData = new byte[] { }, ImageMimeType = "image/png" };

            // Arrange - create the mock repository
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] { 
                new Product { ProductID = 1, Name = "P1" },
                product, 
                new Product { ProductID = 3, Name = "P3" }
            }.AsQueryable());

            // Arrange - create a controller
            var target = new ProductController(mock.Object);

            // Act - call the GetImage action method
            var result = target.GetImage(2);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(product.ImageMimeType, (result as FileResult).ContentType);
        }

        [TestMethod]
        public void Can_Paginate()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
                    new Product { ProductID = 1, Name = "P1" },
                    new Product { ProductID = 2, Name = "P2" },
                    new Product { ProductID = 3, Name = "P3" },
                    new Product { ProductID = 4, Name = "P4" },
                    new Product { ProductID = 5, Name = "P5" }
                }.AsQueryable());

            var target = new ProductController(mock.Object);
            target.PageSize = 3;

            var result = target.List(null, 2).Model as ProductListViewModel;
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
            var target = default(HtmlHelper);

            // Arrange - create the PagingInfo data
            var pagingInfo = new PagingInfo { 
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Arrange - set up the delegate using a lambda expression
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Act
            var result = target.PageLinks(pagingInfo, pageUrlDelegate);

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
                    new Product { ProductID = 1, Name = "P1" },
                    new Product { ProductID = 2, Name = "P2" },
                    new Product { ProductID = 3, Name = "P3" },
                    new Product { ProductID = 4, Name = "P4" },
                    new Product { ProductID = 5, Name = "P5" }
                }.AsQueryable());

            // Arrange
            var target = new ProductController(mock.Object);
            target.PageSize = 3;

            // Act
            var result = target.List(null, 2).Model as ProductListViewModel;

            // Assert
            var pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Products()
        { 
            // Arrange
            // - create the mock repository
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
                    new Product { ProductID = 1, Name = "P1", Category = "Cat1" },
                    new Product { ProductID = 2, Name = "P2", Category = "Cat2" },
                    new Product { ProductID = 3, Name = "P3", Category = "Cat1" },
                    new Product { ProductID = 4, Name = "P4", Category = "Cat2" },
                    new Product { ProductID = 5, Name = "P5", Category = "Cat3" }
                }.AsQueryable());

            // Arrange - create a controller and make the page size 3 items
            var target = new ProductController(mock.Object);
            target.PageSize = 3;

            // Action
            var result = (target.List("Cat2", 1).Model as ProductListViewModel).Products.ToArray();

            // Assert
            Assert.AreEqual(2, result.Length);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category == "Cat2");
        }

        [TestMethod]
        public void Generate_Category_Specific_Product_Count()
        {
            // Arrange
            // - create the mock repository
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
                    new Product { ProductID = 1, Name = "P1", Category = "Cat1" },
                    new Product { ProductID = 2, Name = "P2", Category = "Cat2" },
                    new Product { ProductID = 3, Name = "P3", Category = "Cat1" },
                    new Product { ProductID = 4, Name = "P4", Category = "Cat2" },
                    new Product { ProductID = 5, Name = "P5", Category = "Cat3" }
                }.AsQueryable());

            // Arrange - create a controller and make the page size 3 items
            var target = new ProductController(mock.Object);
            target.PageSize = 3;

            // Action
            var count1 = (target.List("Cat1").Model as ProductListViewModel).PagingInfo.TotalItems;
            var count2 = (target.List("Cat2").Model as ProductListViewModel).PagingInfo.TotalItems;
            var count3 = (target.List("Cat3").Model as ProductListViewModel).PagingInfo.TotalItems;
            var countAll = (target.List(null).Model as ProductListViewModel).PagingInfo.TotalItems;

            // Assert

            Assert.AreEqual(2, count1);
            Assert.AreEqual(2, count2);
            Assert.AreEqual(1, count3);
            Assert.AreEqual(5, countAll);
        }
    }
}
