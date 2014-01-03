using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrlAjaxHelperMethods.Models;

namespace UrlAjaxHelperMethods.Controllers
{
    public class PeopleController : Controller
    {
        private Person[] _personData = {
                                           new Person { FirstName = "Adam", LastName = "Freeman", Role = Role.Admin },
                                           new Person { FirstName = "Steven", LastName = "Sanderson", Role = Role.Admin },
                                           new Person { FirstName = "Jacqui", LastName = "Griffyth", Role = Role.User },
                                           new Person { FirstName = "John", LastName = "Smith", Role = Role.User },
                                           new Person { FirstName = "Anne", LastName = "Jones", Role = Role.Guest }
                                       };
        //
        // GET: /People/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPeople(string selectedRole = "All")
        {
            return View((object)selectedRole);
        }

        public JsonResult GetPeopleDataJson(string selectedRole = "All")
        {
            var data = GetData(selectedRole).Select(p =>
                new {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Role = Enum.GetName(typeof(Role), p.Role)
                });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetPeopleData(string selectedRole = "All")
        {
            return PartialView(GetData(selectedRole));
        }

        private IEnumerable<Person> GetData(string selectedRole)
        {
            var data = _personData as IEnumerable<Person>;
            if (selectedRole != "All")
            {
                var selected = (Role)Enum.Parse(typeof(Role), selectedRole);
                data = _personData.Where(p => p.Role == selected);
            }
            return data;
        }
    }
}
