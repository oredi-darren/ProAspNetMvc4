using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TemplatedHelperMethods.Models
{
    [DisplayName("New Person")]
    public class Person
    {
        [HiddenInput(DisplayValue=false)]
        public int PersonId { get; set; }
        [Display(Name="First")]
        [UIHint("MultilineText")]
        public string FirstName { get; set; }
        [Display(Name = "Last")]
        public string LastName { get; set; }
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public Address HomeAddress { get; set; }
        [Display(Name = "Approved")]
        public bool? IsApproved { get; set; }
        [UIHint("Enum")]
        public Role Role { get; set; }
    }
}