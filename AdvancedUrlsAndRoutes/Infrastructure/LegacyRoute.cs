using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UrlsAndRoutes.Infrastructure
{
    public class LegacyRoute : RouteBase
    {
        private string[] _urls;
        public LegacyRoute(params string[] urls)
        {
            _urls = urls;
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var requestedURL = httpContext.Request.AppRelativeCurrentExecutionFilePath;
            if (_urls.Contains(requestedURL, StringComparer.OrdinalIgnoreCase))
            {
                var result = new RouteData(this, new MvcRouteHandler());
                result.Values.Add("controller", "Legacy");
                result.Values.Add("action", "GetLegacyURL");
                result.Values.Add("LegacyURL", requestedURL);
                return result;
            }
            return null;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            if (values.ContainsKey("legacyURL") && _urls.Contains(values["legacyURL"] as string, StringComparer.OrdinalIgnoreCase))
            {
                return new VirtualPathData(this, new UrlHelper(requestContext).Content(values["legacyURL"] as string).Substring(1));
            }
            return null;
        }
    }
}