using ServiceContracts.DTO;
using ServiceContracts.Enums;
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

        /// <summary>
        /// Retrieves a list of persons filtered according to the specified search criteria.
        /// </summary>
        /// <param name="searchBy">The field name to filter by. Common values include property names such as "FirstName", "LastName", or
        /// "Email". This value is case-insensitive.</param>
        /// <param name="searchString">The value to search for within the specified field. If null or empty, no filtering is applied and all
        /// persons are returned.</param>
        /// <returns>A list of <see cref="PersonResponse"/> objects that match the specified filter. Returns an empty list if no
        /// persons match the criteria.</returns>
        List<PersonResponse> GetFilteredPersonsList(string searchBy, string? searchString);

        /// <summary>
        /// Returns a new list of persons sorted according to the specified property and sort order.
        /// </summary>
        /// <remarks>The original list is not modified. If an invalid property name is provided for
        /// sortBy, the method may throw an exception or ignore the sort, depending on implementation.</remarks>
        /// <param name="allPersons">The list of persons to sort. Cannot be null.</param>
        /// <param name="sortBy">The name of the property to sort by. Common values include "FirstName", "LastName", or "DateOfBirth". Cannot
        /// be null or empty.</param>
        /// <param name="sortOrder">The sort order to apply to the results. Specify ascending or descending.</param>
        /// <returns>A new list of persons sorted by the specified property and order. If the input list is empty, returns an
        /// empty list.</returns>
        List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder);

        PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest);
    }
}