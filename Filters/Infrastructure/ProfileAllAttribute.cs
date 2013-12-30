using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters.Infrastructure
{
    public class ProfileAllAttribute
        : ActionFilterAttribute
    {
        private Stopwatch _timer;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _timer = Stopwatch.StartNew();
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            _timer.Stop();
            filterContext.HttpContext.Response.Write(string.Format("<div>Total elapsed time: {0}</div>", _timer.Elapsed.TotalSeconds));
        }
    }
}