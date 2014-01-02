using System;
using System.Web.Mvc;
using TemplatedHelperMethods.Models;

namespace TemplatedHelperMethods.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.Fruits = new[] { "Apple", "Orange", "Pear" };
            ViewBag.Cities = new[] { "New York", "London", "Paris" };

            var message = "This is an HTML element: <input>";
            return View(message as Object);
        }

        public ActionResult CreatePerson()
        {
            return View(new Person { IsApproved = true });
        }

        [HttpPost]
        public ActionResult CreatePerson(Person person)
        {
            return View("DisplayPerson", person);
        }

        public ActionResult ModelCreatePerson()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult ModelCreatePerson(Person person)
        {
            return View("DisplayPerson", person);
        }
    }
}
