using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ControllersAndActions.Controllers
{
    public class BasicController
        : IController
    {
        public void Execute(RequestContext requestContext)
        {
            var controller = requestContext.RouteData.Values["controller"] as string;
            var action = requestContext.RouteData.Values["action"] as string;

            if (action.ToLower() == "redirect")
            {
                requestContext.HttpContext.Response.Redirect("/Derived/Index");
            }
            else
            {
                requestContext.HttpContext.Response.Write(string.Format("Controller: {0}, Action: {1}", controller, action));
            }
        }
    }
}