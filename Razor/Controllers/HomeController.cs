using Razor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Razor.Controllers
{
    public class HomeController : Controller
    {
        Product _product = new Product
        {
            ProductID = 1,
            Name = "Kayak",
            Description = "A boat for one person",
            Category = "Watersports",
            Price = 275M
        };
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View(_product);
        }

        public ActionResult NameAndPrice()
        {
            return View(_product);
        }

        public ActionResult DemoExpression()
        {
            ViewBag.ProductCount = 1;
            ViewBag.ExpressShip = true;
            ViewBag.ApplyDiscount = false;
            ViewBag.Supplier= null;
            return View(_product);
        }

        public ActionResult DemoArray()
        {
            var products = new Product[]
            {
                new Product { Name = "Kayak", Price = 275M, Category = "Watersports" },
                new Product { Name = "Lifejacket", Price = 48.95M, Category = "Watersports" },
                new Product { Name = "Soccer ball", Price = 19.50M, Category = "Soccer" },
                new Product { Name = "Corner flag", Price = 34.95M, Category = "Soccer" }
            };
            return View(products);
        }   
    }
}
