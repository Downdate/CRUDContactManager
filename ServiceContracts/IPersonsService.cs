using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceContracts
{
    public interface IPersonsService
    {
        /// <summary>
        /// Adds a new person to the system using the specified request data.
        /// </summary>
        /// <param name="personAddRequest">The request containing the details of the person to add. Cannot be null.</param>
        /// <returns>A response object containing information about the added person, including any generated identifiers or
        /// status information.</returns>
        public PersonResponse AddPerson(PersonAddRequest? personAddRequest);

        /// <summary>
        /// Retrieves a list of people represented as response objects.
        /// </summary>
        /// <returns>A list of <see cref="PersonResponse"/> objects containing information about each person. The list is empty
        /// if no people are found.</returns>
        public List<PersonResponse> GetPersonList();

        /// <summary>
        /// Retrieves the person record associated with the specified person identifier.
        /// </summary>
        /// <param name="personID">The unique identifier of the person to retrieve. Specify <see langword="null"/> to indicate that no
        /// identifier is provided.</param>
        /// <returns>A <see cref="PersonResponse"/> containing the details of the person if found; otherwise, <see
        /// langword="null"/>.</returns>
        PersonResponse? GetPersonByPersonID(Guid? personID);

        List<PersonResponse> GetFilteredPersonsList(string searchBy, string? searchString);
    }
}