﻿using MvcModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcModels.Controllers
{
    public class HomeController : Controller
    {
        private IEnumerable<Person> _personData = new []{
                                           new Person { PersonId = 1, FirstName = "Adam", LastName = "Freeman", Role = Role.Admin },
                                           new Person { PersonId = 2, FirstName = "Steven", LastName = "Sanderson", Role = Role.Admin },
                                           new Person { PersonId = 3, FirstName = "Jacqui", LastName = "Griffyth", Role = Role.User },
                                           new Person { PersonId = 4, FirstName = "John", LastName = "Smith", Role = Role.User },
                                           new Person { PersonId = 5, FirstName = "Anne", LastName = "Jones", Role = Role.Guest }
                                       };
        //
        // GET: /Home/

        public ActionResult Index(int? id)
        {
            var dataItem = _personData.Where(p => p.PersonId == id).FirstOrDefault();
            return View(dataItem);
        }

        public ActionResult CreatePerson()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult CreatePerson(Person model)
        {
            return View("Index", model);
        }

        public ActionResult DisplaySummary([Bind(Prefix="HomeAddress", Exclude="Country")]AddressSummary summary)
        {
            return View(summary);
        }

        public ActionResult Names(IEnumerable<string> names)
        {
            names = names ?? new List<string>();
            return View(names);
        }

        public ActionResult Address()
        {
            var addresses = new List<AddressSummary>();
            UpdateModel(addresses);
            return View(addresses);
        }
    }
}
