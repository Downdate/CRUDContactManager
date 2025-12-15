using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    /// <summary>
    /// Represents a person with identifying and contact information.
    /// </summary>
    public class Person
    {
        public Guid ID { get; set; }

        public string? Name { get; set; }

        public string? EmailAddress { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public String? Gender { get; set; }

        public Guid? CountryID { get; set; }
        //public Country? Country { get; set; }

        public string? Address { get; set; }

        public bool ReceiveNewsLetters { get; set; }
    }
}