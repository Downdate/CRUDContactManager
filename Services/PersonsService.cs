using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Services.Helpers;
using ServiceContracts.Enums;

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
            List<PersonResponse> allPersons = GetPersonList();
            List<PersonResponse> filteredPersons = allPersons;

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
            {
                return allPersons;
            }

            switch (searchBy)
            {
                case nameof(Person.Name):
                    filteredPersons = allPersons.Where(temp => temp.Name != null && temp.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(Person.EmailAddress):
                    filteredPersons = allPersons.Where(temp => temp.EmailAddress != null && temp.EmailAddress.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(Person.DateOfBirth):
                    filteredPersons = allPersons.Where(temp => temp.DateOfBirth != null && temp.DateOfBirth.ToString()!.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(Person.Gender):
                    filteredPersons = allPersons.Where(temp => (!string.IsNullOrEmpty(temp.Gender.ToString())) ?
                    temp.Gender.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(Person.Address):
                    filteredPersons = allPersons.Where(temp => temp.Address != null && temp.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(Person.CountryID):
                    filteredPersons = allPersons.Where(temp => (!string.IsNullOrEmpty(temp.CountryName)) ?
                    temp.CountryName.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                default:
                    filteredPersons = allPersons;
                    break;
            }
            return filteredPersons;
        }

        public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy))
            {
                return allPersons;
            }

            List<PersonResponse> sortedPersons =
                (sortBy, sortOrder)
                switch
                {
                    (nameof(PersonResponse.Name), SortOrderOptions.ASCENDING) => allPersons.OrderBy(temp => temp.Name, StringComparer.OrdinalIgnoreCase).ToList(),
                    (nameof(PersonResponse.Name), SortOrderOptions.DESCENDING) => allPersons.OrderByDescending(temp => temp.Name, StringComparer.OrdinalIgnoreCase).ToList(),
                    (nameof(PersonResponse.EmailAddress), SortOrderOptions.ASCENDING) => allPersons.OrderBy(temp => temp.EmailAddress, StringComparer.OrdinalIgnoreCase).ToList(),
                    (nameof(PersonResponse.EmailAddress), SortOrderOptions.DESCENDING) => allPersons.OrderByDescending(temp => temp.EmailAddress, StringComparer.OrdinalIgnoreCase).ToList(),
                    (nameof(PersonResponse.DateOfBirth), SortOrderOptions.ASCENDING) => allPersons.OrderBy(temp => temp.DateOfBirth).ToList(),
                    (nameof(PersonResponse.DateOfBirth), SortOrderOptions.DESCENDING) => allPersons.OrderByDescending(temp => temp.DateOfBirth).ToList(),
                    (nameof(PersonResponse.Age), SortOrderOptions.ASCENDING) => allPersons.OrderBy(temp => temp.Age).ToList(),
                    (nameof(PersonResponse.Age), SortOrderOptions.DESCENDING) => allPersons.OrderByDescending(temp => temp.Age).ToList(),
                    (nameof(PersonResponse.Gender), SortOrderOptions.ASCENDING) => allPersons.OrderBy(temp => temp.Gender).ToList(),
                    (nameof(PersonResponse.Gender), SortOrderOptions.DESCENDING) => allPersons.OrderByDescending(temp => temp.Gender).ToList(),
                    (nameof(PersonResponse.CountryName), SortOrderOptions.ASCENDING) => allPersons.OrderBy(temp => temp.CountryName, StringComparer.OrdinalIgnoreCase).ToList(),
                    (nameof(PersonResponse.CountryName), SortOrderOptions.DESCENDING) => allPersons.OrderByDescending(temp => temp.CountryName, StringComparer.OrdinalIgnoreCase).ToList(),
                    (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.ASCENDING) => allPersons.OrderBy(temp => temp.ReceiveNewsLetters).ToList(),
                    (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.DESCENDING) => allPersons.OrderByDescending(temp => temp.ReceiveNewsLetters).ToList(),

                    _ => allPersons
                };

            return sortedPersons;
        }
    }
}