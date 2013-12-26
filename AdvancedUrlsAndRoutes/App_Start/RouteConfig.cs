using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AdvancedUrlsAndRoutes.Infrastructure;
using UrlsAndRoutes.Infrastructure;

namespace AdvancedUrlsAndRoutes
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.MapRoute("NewRoute", "App/Do{action}",
            //    new { controller = "Home" });
            routes.Add(new Route("SayHello", new CustomRouteHandler()));
            routes.Add(new LegacyRoute("~/articles/Windows_3.1_Overview.html",
                "~/old/.NET_1.0_Class_Library"));
            routes.MapRoute("MyRoute", "{controller}/{action}/{id}"
                , new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                , new[] { "AdvancedUrlsAndRoutes.Controllers" }
            );
        }
    }
}