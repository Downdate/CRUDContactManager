using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Services.Helpers;

namespace Services
{
    public class PersonsService : IPersonsService
    {
        //private field

        private readonly List<Person> _personsList;
        private readonly ICountriesService _countriesService;

        //constructor
        public PersonsService()
        {
            _personsList = new List<Person>();
            _countriesService = new CountriesService();
        }

        private PersonResponse ConvertPersonIntoPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.CountryName = _countriesService.GetCountryByCountryID(personResponse.CountryID)?.CountryName;
            return personResponse;
        }

        /// <summary>
        /// Adds a new person to the collection using the specified request data.
        /// </summary>
        /// <param name="personAddRequest">The request containing the details of the person to add. Cannot be null.</param>
        /// <returns>A response object containing the details of the newly added person.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="personAddRequest"/> is null.</exception>
        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            if (personAddRequest == null)
            {
                throw new ArgumentNullException(nameof(personAddRequest));
            }

            //Model validation
            ValidationHelper.ModelValidation(personAddRequest);

            //convert to person

            Person person = personAddRequest.ToPerson();

            //Generate person.ID

            person.ID = Guid.NewGuid();

            //Add to list
            _personsList.Add(person);

            //convert to PersonResponse
            return person.ToPersonResponse();
        }

        /// <summary>
        /// Retrieves a list of persons represented as response objects.
        /// </summary>
        /// <returns>A list of <see cref="PersonResponse"/> objects representing all persons. Returns an empty list if no persons
        /// are available.</returns>
        public List<PersonResponse> GetPersonList()
        {
            if (_personsList.Count == 0)
            {
                return new List<PersonResponse>();
            }

            return _personsList.Select(temp => temp.ToPersonResponse()).ToList();
        }

        /// <summary>
        /// Retrieves the person details for the specified person identifier.
        /// </summary>
        /// <param name="personID">The unique identifier of the person to retrieve. Can be null.</param>
        /// <returns>A <see cref="PersonResponse"/> containing the person's details if found; otherwise, <see langword="null"/>.</returns>
        public PersonResponse? GetPersonByPersonID(Guid? personID)
        {
            if (personID == null)
            {
                return null;
            }

            Person? person = _personsList.FirstOrDefault(temp => temp.ID == personID);

            if (person == null)
            {
                return null;
            }

            return person.ToPersonResponse();
        }

        /// <summary>
        /// Retrieves a list of persons filtered according to the specified search criteria.
        /// </summary>
        /// <param name="searchBy">The field name to filter by, such as "Name", "Email", or another supported property. This value determines
        /// which property of the person records will be searched.</param>
        /// <param name="searchString">The value to search for within the specified field. If null or empty, no filtering is applied and all
        /// persons are returned.</param>
        /// <returns>A list of persons matching the specified filter criteria. Returns an empty list if no persons match the
        /// criteria.</returns>
        /// <exception cref="NotImplementedException">The method is not implemented.</exception>
        public List<PersonResponse> GetFilteredPersonsList(string searchBy, string? searchString)
        {
            throw new NotImplementedException();
        }
    }
}