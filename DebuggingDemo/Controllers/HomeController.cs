using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DebuggingDemo.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var firstVal = 10;
            var secondVal = 5;
            var result = firstVal / secondVal;
            //ViewBag.Message = "Welcome to ASP.NET MVC!";
            return View(result);
        }

    }
}
