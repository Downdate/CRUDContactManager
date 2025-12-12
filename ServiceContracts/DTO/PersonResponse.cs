using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using System.Runtime.CompilerServices;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// Represents the response containing information about a person. DTO for person response.
    /// </summary>
    public class PersonResponse
    {
        public Guid ID { get; set; }
        public string? Name { get; set; }
        public string? EmailAddress { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public GenderOptions? Gender { get; set; }

        public string? Address { get; set; }

        public Guid? CountryID { get; set; }

        public string? CountryName { get; set; }

        public bool ReceiveNewsLetters { get; set; }

        public double? Age { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current PersonResponse instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current PersonResponse instance.</param>
        /// <returns>true if the specified object is a PersonResponse and has the same properties as the current instance; otherwise,
        /// false.</returns>
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (obj.GetType() != typeof(PersonResponse)) return false;

            PersonResponse person = (PersonResponse)obj;

            return this.ID == person.ID
                && this.Name == person.Name
                && this.EmailAddress == person.EmailAddress
                && this.DateOfBirth == person.DateOfBirth
                && this.Gender == person.Gender
                && this.Address == person.Address
                && this.CountryID == person.CountryID
                && this.CountryName == person.CountryName
                && this.ReceiveNewsLetters == person.ReceiveNewsLetters
                && this.Age == person.Age;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"ID={ID}, Name={Name}, EmailAddress={EmailAddress}, DateOfBirth={DateOfBirth}";
        }

        public PersonUpdateRequest ToPersonUpdateRequest()
        {
            return new PersonUpdateRequest()
            {
                ID = this.ID,
                Name = this.Name,
                EmailAddress = this.EmailAddress,
                DateOfBirth = this.DateOfBirth,
                Gender = (GenderOptions)Enum.Parse(typeof(GenderOptions), Gender.ToString(), true),
                Address = this.Address,
                CountryID = this.CountryID,
                ReceiveNewsLetters = this.ReceiveNewsLetters
            };
        }
    }

    public static class PersonExtentions
    {
        /// <summary>
        /// Converts a Person object to a PersonResponse object for use in API responses or data transfer scenarios.
        /// </summary>
        /// <param name="person">The Person instance to convert. Cannot be null.</param>
        /// <returns>A PersonResponse object containing the mapped data from the specified Person instance.</returns>
        public static PersonResponse ToPersonResponse(this Person person)
        {
            return new PersonResponse()
            {
                ID = person.ID,
                Name = person.Name,
                EmailAddress = person.EmailAddress,
                DateOfBirth = person.DateOfBirth,
                Gender = Enum.Parse<GenderOptions>(person.Gender),
                Address = person.Address,
                CountryID = person.CountryID,
                //CountryName = person.CountryName,
                ReceiveNewsLetters = person.ReceiveNewsLetters,
                Age = (person.DateOfBirth != null) ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25) : null,
            };
        }
    }
}