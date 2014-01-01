using HelperMethods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelperMethods.Controllers
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

        public ActionResult External()
        {
            ViewBag.Fruits = new[] { "Apple", "Orange", "Pear" };
            ViewBag.Cities = new[] { "New York", "London", "Paris" };

            var message = "This is an HTML element: <input>";
            return View(message as Object);
        }

        public ActionResult HtmlEncode()
        {
            var message = "This is an HTML element: <input>";
            return View(message as Object);
        }

        public ActionResult CreatePerson()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult CreatePerson(Person person)
        {
            return View(person);
        }

        public ActionResult BasicBeginForm()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult BasicBeginForm(Person person)
        {
            return View(person);
        }

        public ActionResult BasicUsingBeginForm()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult BasicUsingBeginForm(Person person)
        {
            return View(person);
        }

        public ActionResult ComplexUsingBeginForm()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult ComplexUsingBeginForm(Person person)
        {
            return View(person);
        }

        public ActionResult BeginRouteForm()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult BeginRouteForm(Person person)
        {
            return View(person);
        }

        public ActionResult InputHelpers()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult InputHelpers(Person person)
        {
            return View(person);
        }

        public ActionResult ModelPropertyInputHelpers()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult ModelPropertyInputHelpers(Person person)
        {
            return View(person);
        }

        public ActionResult TypedInputHelpers()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult TypedInputHelpers(Person person)
        {
            return View(person);
        }

        public ActionResult SelectInputHelpers()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult SelectInputHelpers(Person person)
        {
            return View(person);
        }
    }
}
