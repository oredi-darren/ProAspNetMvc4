using ModelValidation.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ModelValidation.Infrastructure
{
    public class NoJoeOnMondays : ValidationAttribute
    {
        public NoJoeOnMondays()
        {
            ErrorMessage = "Joe cannot book appointments on Mondays";
        }

        public override bool IsValid(object value)
        {
            var appointment = value as Appointment;
            if (appointment == null || string.IsNullOrEmpty(appointment.ClientName) || appointment.Date == null)
            {
                // we don't have a model of the right type to validate, or we don't have
                // the values for ClientName and Date properties we require
                return true;
            }
            return !(appointment.ClientName == "Joe" && appointment.Date.DayOfWeek == DayOfWeek.Monday);
        }
    }
}