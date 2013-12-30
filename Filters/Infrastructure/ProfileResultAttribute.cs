﻿using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace Filters.Infrastructure
{
    public class ProfileResultAttribute
        : FilterAttribute, IResultFilter
    {
        private Stopwatch _timer;
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            _timer.Stop();
            filterContext.HttpContext.Response.Write(string.Format("<div>Result elapsed time: {0}</div>", _timer.Elapsed.TotalSeconds));
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            _timer = Stopwatch.StartNew();
        }
    }
}