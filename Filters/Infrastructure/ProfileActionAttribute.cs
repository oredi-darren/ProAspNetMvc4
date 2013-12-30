using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace Filters.Infrastructure
{
    public class ProfileActionAttribute
        : FilterAttribute, IActionFilter
    {
        private Stopwatch _timer;
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _timer.Stop();
            if (filterContext.Exception == null)
            {
                filterContext.HttpContext.Response.Write(string.Format("<div>Action method elapsed time: {0}</div>", _timer.Elapsed.TotalSeconds));
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _timer = Stopwatch.StartNew(); ;
        }
    }
}