using Filters.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [CustomAuth(true)]
        public string Index()
        {
            return "This is the Index action on the Home controller";
        }

        [HandleError(ExceptionType = typeof(ArgumentOutOfRangeException), View="RangeError")]
        public string RangeTest(int id)
        {
            if (id <= 100)
            {
                throw new ArgumentOutOfRangeException("id", id, "");
            }

            return string.Format("The id value is: {0}", id);
        }

        [CustomAction]
        public string FilterTest()
        {
            return "This is the ActionFilterTest action";
        }

        [ProfileAction]
        [ProfileResult]
        [ProfileAll]
        public string ProfileActionFilterTest()
        {
            return "This is the ProfileActionFilterTest action";
        }
    }
}
