using ModelValidation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModelValidation.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult MakeBooking()
        {
            return View(new Appointment { Date = DateTime.Now });
        }

        [HttpPost]
        public ViewResult MakeBooking(Appointment appointment)
        {
            // Explicitly validating a model
            //if (string.IsNullOrWhiteSpace(appointment.ClientName))
            //{
            //    ModelState.AddModelError("ClientName", "Please enter your name");
            //}

            //if (ModelState.IsValidField("Date") && DateTime.Now > appointment.Date)
            //{
            //    ModelState.AddModelError("Date", "Please enter a date in the future");
            //}

            //if (!appointment.TermsAccepted)
            //{
            //    ModelState.AddModelError("TermsAccepted", "You must accept the terms");
            //}

            //if (ModelState.IsValidField("ClientName") && ModelState.IsValidField("Date")
            //    && appointment.ClientName == "Joe" && appointment.Date.DayOfWeek == DayOfWeek.Monday)
            //{
            //    ModelState.AddModelError("", "Joe cannot book appointments on Mondays");
            //}

            if(ModelState.IsValid)
                // statements to store new Appointment in a
                // repository woul go here in a real project
                return View("Completed", appointment);

            return View();
        }

        public JsonResult ValidateDate(string Date)
        {
            var parsedDate = default(DateTime);

            if (!DateTime.TryParse(Date, out parsedDate))
            {
                return Json("Please enter a valid date (mm/dd/yyyy)", JsonRequestBehavior.AllowGet);
            }
            else if (DateTime.Now > parsedDate)
            {
                return Json("Please enter a date in the future", JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
