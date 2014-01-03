﻿using System;
using System.ComponentModel.DataAnnotations;

namespace UrlAjaxHelperMethods.Models
{
    [MetadataType(typeof(PersonMetaData))]
    public partial class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Address HomeAddress { get; set; }
        public bool? IsApproved { get; set; }
        public Role Role { get; set; }
    }
}