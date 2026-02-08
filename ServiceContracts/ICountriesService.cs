using ServiceContracts.DTO;
using Microsoft.AspNetCore.Http;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Country entity
    /// </summary>
    public interface ICountriesService
    {
        /// <summary>
        /// Adds a country object to the list of countries
        /// </summary>
        /// <param name="countryAddRequest">Country object to be added</param>
        /// <returns>Returns the country object after adding it (including newly generated country id) </returns>
        Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest);

        /// <summary>
        /// Returns all countries from the list
        /// </summary>
        /// <returns>Returns all countries from the list as a list of CountryResponse</returns>
        Task<List<CountryResponse>> GetAllCountries();

        /// <summary>
        /// Retrieves country information for the specified country identifier.
        /// </summary>
        /// <param name="countryID">The unique identifier of the country to retrieve. Specify <see langword="null"/> to indicate that no country
        /// is selected.</param>
        /// <returns>A <see cref="CountryResponse"/> containing details of the country if found; otherwise, <see
        /// langword="null"/>.</returns>
        Task<CountryResponse?> GetCountryByCountryID(Guid? countryID);

        /// <summary>
        /// Uploads country data from the specified Excel file asynchronously and returns the number of countries
        /// successfully processed.
        /// </summary>
        /// <remarks>Ensure that the provided Excel file adheres to the expected format for country data.
        /// The method may throw exceptions if the file is invalid or if errors occur during the upload
        /// process.</remarks>
        /// <param name="formFile">The Excel file containing country data to be uploaded. This parameter must not be null and should be in a
        /// valid Excel format.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the number of countries that
        /// were successfully uploaded from the Excel file.</returns>
        Task<int> UploadCountriesFromExcelFileAsync(IFormFile formFile);
    }
}