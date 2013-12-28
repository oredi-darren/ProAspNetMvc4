using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters.Infrastructure
{
    public class CustomAuthAttribute
        : AuthorizeAttribute
    {
        private bool _localAllowed;
        public CustomAuthAttribute(bool localAllowed)
        {
            _localAllowed = localAllowed;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Request.IsLocal)
            {
                return _localAllowed;
            }
            return true;
        }
    }
}