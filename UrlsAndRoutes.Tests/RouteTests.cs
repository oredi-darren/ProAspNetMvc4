using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using Moq;
using System.Web.Routing;

namespace UrlsAndRoutes.Tests
{
    [TestClass]
    public class RouteTests
    {
        #region Support
        private HttpContextBase CreateHttpContext(string targetUrl = null, string httpMethod = "GET")
        {
            // create the mock request
            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath)
                .Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

            // create the mock response
            var mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

            // create the mock contextm using the request and response
            var mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            // return the mocked context
            return mockContext.Object;
        }

        private void TestRouteMatch(string url, string controller, string action, object routeProperties = null, string httpMethod = "GET")
        {
            // Arrange
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act - process the route
            var result = routes.GetRouteData(CreateHttpContext(url, httpMethod));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, controller, action, routeProperties));
        }

        private void TestRouteFail(string url)
        {
            // Arrange
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act - process the route
            var result = routes.GetRouteData(CreateHttpContext(url));

            // Assert
            Assert.IsTrue(result == null || result.Route == null);
        }

        private bool TestIncomingRouteResult(RouteData routeResult, string controller, string action, object propertySet = null)
        {
            Func<object, object, bool> valCompare = (v1, v2) =>
            {
                return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;
            };

            if ((valCompare(routeResult.Values["controller"], controller) && valCompare(routeResult.Values["action"], action)) == false)
                return false;

            if (propertySet != null)
            {
                var propertyInfo = propertySet.GetType().GetProperties();
                foreach (var instance in propertyInfo)
                {
                    if (!(routeResult.Values.ContainsKey(instance.Name) && valCompare(routeResult.Values[instance.Name], instance.GetValue(propertySet, null))))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        [TestMethod]
        public void TestIncomingRoutes()
        {
            // check that defaults work
            TestRouteMatch("~/", "Home", "Index");
            TestRouteMatch("~/Customer", "Customer", "Index");
            TestRouteMatch("~/Shop/Index", "Home", "Index");
            TestRouteMatch("~/Shop/OldAction", "Home", "Index");
            TestRouteMatch("~/Public", "Home", "Index");
            TestRouteMatch("~/XHome/Index", "Home", "Index");
            
            // check for the URL we hope to receive
            TestRouteMatch("~/Customer/List", "Customer", "List");
            TestRouteMatch("~/Customer/List/All", "Customer", "List", new { id = "All" });
            TestRouteMatch("~/Admin/Index", "Admin", "Index");

            // check that the values are being obtained from the segments
            TestRouteMatch("~/One/Two", "One", "Two");

            // catchall testing
            TestRouteMatch("~/Customer/List/All/Delete", "Customer", "List", new { id = "All", catchall = "Delete" });
            TestRouteMatch("~/Customer/List/All/Delete/Perm", "Customer", "List", new { id = "All", catchall = "Delete/Perm" });
            // ensure that too many or too few segments fails to match
            // Catch all invalidates the following checks
            //TestRouteFail("~/Admin/Index/All/Segment");
            //TestRouteFail("~/Customer/Index/All/Segment");
        }
    }
}
