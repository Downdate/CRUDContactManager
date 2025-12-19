using System;
using System.Collections.Generic;
using System.Text;
using ServiceContracts.Enums;
using Entities;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// Represents a request to add a new person to the system. DTO for adding person.
    /// </summary>
    public class PersonAddRequest
    {
        [Required(ErrorMessage = "PersonName can't be blank")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Person Email can't be blank")]
        [EmailAddress(ErrorMessage = "Email should be valid")]
        [DataType(DataType.EmailAddress)]
        public string? EmailAddress { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date of Birth can't be blank")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Choose a Gender")]
        public GenderOptions? Gender { get; set; }

        public string? Address { get; set; }

        [Required(ErrorMessage = "Please select a Country!")]
        public Guid? CountryID { get; set; }

        public bool ReceiveNewsLetters { get; set; }

        /// <summary>
        /// Converts the current instance to a new <see cref="Person"/> object with equivalent property values.
        /// </summary>
        /// <returns>A <see cref="Person"/> object containing the data from the current instance.</returns>
        public Person ToPerson()
        {
            Person person = new Person()
            {
                Name = this.Name,
                EmailAddress = this.EmailAddress,
                DateOfBirth = this.DateOfBirth,
                Gender = this.Gender.ToString(),
                Address = this.Address,
                CountryID = this.CountryID,
                Country = null,
                ReceiveNewsLetters = this.ReceiveNewsLetters,
            };

            return person;
        }
    }
}