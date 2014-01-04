using ModelValidation.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModelValidation.Models
{
    [NoJoeOnMondays]
    public class Appointment
        //: IValidatableObject
    {
        [Required]
        [StringLength(10, MinimumLength=3)]
        public string ClientName { get; set; }

        [DataType(DataType.Date)]
        [Remote("ValidateDate", "Home")]
        public DateTime Date { get; set; }

        [TrueOnlyAttribute(ErrorMessage = "You must accept the terms")]
        public bool TermsAccepted { get; set; }

        //// Self validating models
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    var errors = new List<ValidationResult>();
        //    if (string.IsNullOrWhiteSpace(ClientName))
        //    {
        //        errors.Add(new ValidationResult("Please enter your name"));
        //    }

        //    if (DateTime.Now > Date)
        //    {
        //        errors.Add(new ValidationResult("Please enter a date in the future"));
        //    }

        //    if (errors.Count == 0 && ClientName == "Joe" && Date.DayOfWeek == DayOfWeek.Monday)
        //    {
        //        errors.Add(new ValidationResult("Joe cannot book appointments on Mondays"));
        //    }

        //    if (!TermsAccepted)
        //    {
        //        errors.Add(new ValidationResult("You must accept the terms"));
        //    }

        //    return errors;
        //}
    }
}