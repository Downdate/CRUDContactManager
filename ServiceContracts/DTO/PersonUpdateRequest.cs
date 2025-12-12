using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// Represents a request to update the details of an existing person.
    /// </summary>
    /// <remarks>This class is typically used as a data transfer object when submitting updates to a person's
    /// information, such as name, email address, date of birth, gender, address, country, and newsletter preferences.
    /// All properties are optional unless otherwise specified by validation attributes. Use this type when performing
    /// partial or full updates to a person's record.</remarks>
    public class PersonUpdateRequest
    {
        [Required(ErrorMessage = "ID can't be blank")]
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Name can't be blank")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Person Email can't be blank")]
        [EmailAddress(ErrorMessage = "Email should be valid")]
        public string? EmailAddress { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public GenderOptions? Gender { get; set; }

        public string? Address { get; set; }

        public Guid? CountryID { get; set; }

        public bool ReceiveNewsLetters { get; set; }

        /// <summary>
        /// Converts the current instance to a new <see cref="Person"/> object with equivalent property values.
        /// </summary>
        /// <returns>A <see cref="Person"/> object containing the data from the current instance.</returns>
        public Person ToPerson()
        {
            Entities.Person person = new Entities.Person()
            {
                ID = this.ID,
                Name = this.Name,
                EmailAddress = this.EmailAddress,
                DateOfBirth = this.DateOfBirth,
                Gender = this.Gender.ToString(),
                Address = this.Address,
                CountryID = this.CountryID,
                ReceiveNewsLetters = this.ReceiveNewsLetters,
            };

            return person;
        }
    }
}