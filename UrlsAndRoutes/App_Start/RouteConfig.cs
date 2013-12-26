using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UrlsAndRoutes.Infrastructure;

namespace UrlsAndRoutes
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //Basic method
            //var route = new Route("{controller}/{action}/", new MvcRouteHandler());
            //routes.Add("MyRoute", route);
            routes.MapRoute("ShopSchema2", "Shop/OldAction"
                 , new { controller = "Home", action = "Index" });

            routes.MapRoute("ShopSchema", "Shop/{action}"
                 , new { controller = "Home", action = "Index" });

            routes.MapRoute("", "X{controller}/{action}"
                 , new { controller = "Home", action = "Index" });

            routes.MapRoute("", "Public/{controller}/{action}"
                , new { controller = "Home", action = "Index" });

            routes.MapRoute("AddControllerRoute", "Home/{action}/{id}/{*catchall}"
                , new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                , new { customConstraint = new UserAgentConstraint("Chrome") }
                , new[] { "UrlsAndRoutes.AdditionalControllers" });

            routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}"
                , new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                , new [] { "UrlsAndRoutes.Controllers" });
        }
    }
}