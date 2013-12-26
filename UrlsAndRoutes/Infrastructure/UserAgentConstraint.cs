using System.Web.Routing;

namespace UrlsAndRoutes.Infrastructure
{
    public class UserAgentConstraint : IRouteConstraint
    {
        private string _requiredUserAgent;
        public UserAgentConstraint(string requiredUserAgent)
        {
            _requiredUserAgent = requiredUserAgent;
        }

        public bool Match(System.Web.HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return httpContext.Request.UserAgent != null && httpContext.Request.UserAgent.Contains(_requiredUserAgent);
        }
    }
}