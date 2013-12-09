using LanguageFeatures.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Linq;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public string Index()
        {
            return "Navigate to a URL to show an example";
        }

        public ViewResult AutoProperty()
        {
            // Showing object properties

            // create a new Product object
            var product = new Product();

            // set the property
            product.Name = "Kayak";

            // get the property
            var productName = product.Name;

            // generate the view
            return View("Result",
                string.Format("Product name: {0}", productName) as object);
        }

        public ViewResult CreateProduct()
        {
            // Showing object initializers
            var product = new Product
            {
                ProductID = 100,
                Name = "Kayak",
                Description = "A boat for one person",
                Price = 274M,
                Category = "Watersports"
            };

            return View("Result",
                string.Format("Category: {0}", product.Category) as object);
        }

        public ViewResult CreateCollection()
        {
            // Showing list and collection initializers
            var stringArray = new string[]{ "apple", "orange", "plum" };

            var intList = new List<int> { 10, 20, 30, 40 };

            var myDict = new Dictionary<string, int> { { "apple", 10 }, { "orange", 20 }, {"plum", 30} };

            return View("Result", stringArray[1] as object);
        }

        public ViewResult UseExtension()
        {
            // create and populate ShoppingCart
            var cart = new ShoppingCart
            {
                Products = new List<Product> {
                    new Product { Name = "Kayak", Price = 275M },
                    new Product { Name = "Lifejacket", Price = 48.95M },
                    new Product { Name = "Soccer ball", Price = 19.50M },
                    new Product { Name = "Corner flag", Price = 34.95M }
                }
            };

            // get the total value of products in the cart
            decimal cartTotal = cart.TotalPrices();
            return View("Result", string.Format("Total: {0}", cartTotal) as object);
        }

        public ViewResult UseExtensionEnumerable()
        {
            // create and populate ShoppingCart
            var cart = new Product[]
            {
                new Product { Name = "Kayak", Price = 275M },
                new Product { Name = "Lifejacket", Price = 48.95M },
                new Product { Name = "Soccer ball", Price = 19.50M },
                new Product { Name = "Corner flag", Price = 34.95M }
            };

            // get the total value of products in the cart
            decimal cartTotal = cart.TotalPrices();
            return View("Result", string.Format("Total: {0}", cartTotal) as object);
        }

        public ViewResult UseFilterExtension()
        {
            // create and populate ShoppingCart
            var cart = new Product[]
            {
                new Product { Name = "Kayak", Price = 275M, Category = "Watersports" },
                new Product { Name = "Lifejacket", Price = 48.95M, Category = "Watersports" },
                new Product { Name = "Soccer ball", Price = 19.50M, Category = "Soccer" },
                new Product { Name = "Corner flag", Price = 34.95M, Category = "Soccer" }
            };

            // get the total value of products in the cart
            decimal cartTotal = cart.FilterByCategory("Soccer").TotalPrices();
            return View("Result", string.Format("Total: {0}", cartTotal) as object);
        }

        public ViewResult UseFilterExtensionMethod()
        {
            // create and populate ShoppingCart
            var cart = new Product[]
            {
                new Product { Name = "Kayak", Price = 275M, Category = "Watersports" },
                new Product { Name = "Lifejacket", Price = 48.95M, Category = "Watersports" },
                new Product { Name = "Soccer ball", Price = 19.50M, Category = "Soccer" },
                new Product { Name = "Corner flag", Price = 34.95M, Category = "Soccer" }
            };

            Func<Product, bool> nameFilter = delegate(Product product)
            {
                return product.Name.Contains("Soccer");
            };

            // get the total value of products in the cart
            decimal cartTotal = cart.Filter(nameFilter).TotalPrices();
            return View("Result", string.Format("Total: {0}", cartTotal) as object);
        }

        public ViewResult UseLambaFilterExtension()
        {
            // create and populate ShoppingCart
            var cart = new Product[]
            {
                new Product { Name = "Kayak", Price = 275M, Category = "Watersports" },
                new Product { Name = "Lifejacket", Price = 48.95M, Category = "Watersports" },
                new Product { Name = "Soccer ball", Price = 19.50M, Category = "Soccer" },
                new Product { Name = "Corner flag", Price = 34.95M, Category = "Soccer" }
            };

            // get the total value of products in the cart
            decimal cartTotal = cart.Filter( product => product.Category == "Soccer" || product.Price > 20.0M).TotalPrices();
            return View("Result", string.Format("Total: {0}", cartTotal) as object);
        }

        public ViewResult CreateAnonArray()
        {
            var oddsAndEnds = new[] {
                new { Name = "MVC", Category = "Pattern" },
                new { Name = "Hat", Category = "Clothing" },
                new { Name = "Apple", Category = "Fruit" }
            };

            var result = new StringBuilder();
            foreach (var item in oddsAndEnds)
            {
                result.AppendLine(item.Name);
            }
            return View("Result", result.ToString() as object);
        }

        public ViewResult FindProductsQuery()
        {
            // create and populate ShoppingCart
            var products = new Product[]
            {
                new Product { Name = "Kayak", Price = 275M, Category = "Watersports" },
                new Product { Name = "Lifejacket", Price = 48.95M, Category = "Watersports" },
                new Product { Name = "Soccer ball", Price = 19.50M, Category = "Soccer" },
                new Product { Name = "Corner flag", Price = 34.95M, Category = "Soccer" }
            };

            var foundProducts = from match in products
                                orderby match.Price descending
                                select new
                                {
                                    match.Name,
                                    match.Price
                                };

            // create the result
            var count = 0;
            var result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Price: {0} ", p.Price);
                if (++count == 3) break;
            }
            return View("Result", result.ToString() as object);
        }

        public ViewResult FindProductsDotNotation()
        {
            // create and populate ShoppingCart
            var products = new Product[]
            {
                new Product { Name = "Kayak", Price = 275M, Category = "Watersports" },
                new Product { Name = "Lifejacket", Price = 48.95M, Category = "Watersports" },
                new Product { Name = "Soccer ball", Price = 19.50M, Category = "Soccer" },
                new Product { Name = "Corner flag", Price = 34.95M, Category = "Soccer" }
            };

            var foundProducts = products.OrderByDescending(e => e.Price)
                .Take(3)
                .Select(e => new { e.Name, e.Price });

            // create the result
            var result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Price: {0} ", p.Price);
            }
            return View("Result", result.ToString() as object);
        }

        public ViewResult FindProductsDeferred()
        {
            // create and populate ShoppingCart
            var products = new Product[]
            {
                new Product { Name = "Kayak", Price = 275M, Category = "Watersports" },
                new Product { Name = "Lifejacket", Price = 48.95M, Category = "Watersports" },
                new Product { Name = "Soccer ball", Price = 19.50M, Category = "Soccer" },
                new Product { Name = "Corner flag", Price = 34.95M, Category = "Soccer" }
            };

            var foundProducts = products.OrderByDescending(e => e.Price)
                .Take(3)
                .Select(e => new { e.Name, e.Price });

            products[2] = new Product { Name = "Stadium", Price = 79600M };

            // create the result
            var result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Price: {0} ", p.Price);
            }
            return View("Result", result.ToString() as object);
        }

        public ViewResult SumProducts()
        {
            // create and populate ShoppingCart
            var products = new Product[]
            {
                new Product { Name = "Kayak", Price = 275M, Category = "Watersports" },
                new Product { Name = "Lifejacket", Price = 48.95M, Category = "Watersports" },
                new Product { Name = "Soccer ball", Price = 19.50M, Category = "Soccer" },
                new Product { Name = "Corner flag", Price = 34.95M, Category = "Soccer" }
            };

            var foundProducts = products.OrderByDescending(e => e.Price)
                .Take(3)
                .Select(e => new { e.Name, e.Price });
            var result =  products.Sum(e => e.Price);
            products[2] = new Product { Name = "Stadium", Price = 79600M };
            return View("Result", string.Format("Sum: {0:c}", result.ToString()) as object);
        }
    }
}
