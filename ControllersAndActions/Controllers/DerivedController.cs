using System.Web.Mvc;

namespace ControllersAndActions.Controllers
{
    public class DerivedController : Controller
    {
        //
        // GET: /Derived/

        public ActionResult Index()
        {
            ViewBag.Message = "Hello from the DerivedController Index method";
            return View("MyView");
        }

        public ActionResult ProduceOutput()
        {
            //if (Server.MachineName.ToLower() == "darren-windows")
            //{
            //    return new CustomRedirectResult { Url = "/Basic/Index" };
            //}
            //else
            //{
            //    Response.Write("Controller: Derived, Action: ProductOutput");
            //    return null;
            //}
            return Redirect("/Basic/Index");
        }
    }
}
