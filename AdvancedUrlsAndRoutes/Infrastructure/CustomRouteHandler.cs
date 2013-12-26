using System.Web;
using System.Web.Routing;

namespace AdvancedUrlsAndRoutes.Infrastructure
{
    public class CustomRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new CustomHttpHandler();
        }
    }
}