using EssentialTools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;

namespace EssentialTools.Controllers
{
    public class HomeController : Controller
    {
        private Product[] _products = {
                new Product { Name = "Kayak", Price = 275M, Category = "Watersports" },
                new Product { Name = "Lifejacket", Price = 48.95M, Category = "Watersports" },
                new Product { Name = "Soccer ball", Price = 19.50M, Category = "Soccer" },
                new Product { Name = "Corner flag", Price = 34.95M, Category = "Soccer" }
            };
        private IValueCalculator _calc;

        public HomeController(IValueCalculator calc)
        {
            _calc = calc;
        }

        //
        // GET: /Home/

        public ActionResult Index()
        {
            var cart = new ShoppingCart(_calc) { Products = _products };
            var totalValue = cart.CalculateProductTotal();
            return View(totalValue);
        }

    }
}
