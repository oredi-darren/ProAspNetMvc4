using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UrlsAndRoutes.AdditionalControllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            ViewBag.Controller = "Addtional Controllers - Home";
            ViewBag.Action = "Index";
            return View("ActionName");
        }

        public ActionResult CustomVariable(string id = "DefaultId", string catchall = "")
        {
            ViewBag.Controller = "Addtional Controllers - Home";
            ViewBag.Action = "CustomVariable";
            ViewBag.CustomVariable = id;
            ViewBag.CatchAll = catchall;
            return View();
        }
    }
}
