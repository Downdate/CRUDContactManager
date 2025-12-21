using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    /// <summary>
    /// Represents a person with identifying and contact information.
    /// </summary>
    public class Person
    {
        [Key]
        public Guid ID { get; set; }

        [StringLength(40)]
        public string? Name { get; set; }

        [StringLength(40)]
        public string? EmailAddress { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)]
        public String? Gender { get; set; }

        public Guid? CountryID { get; set; }
        public Country? Country { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        public bool ReceiveNewsLetters { get; set; }
    }
}