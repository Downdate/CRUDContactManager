using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

namespace Services
{
    public class CountriesGetterService : ICountriesGetterService
    {
        //private field
        private readonly ApplicationDbContext _DbContext;

        //constructor
        public CountriesGetterService(ApplicationDbContext personsDbContext)
        {
            _DbContext = personsDbContext;
        }
        public async Task<List<CountryResponse>> GetAllCountries()
        {
            return await _DbContext.Countries
                .Select(country => country.ToCountryResponse()).ToListAsync();
        }

        public async Task<CountryResponse?> GetCountryByCountryID(Guid? countryID)
        {
            if (countryID == null)
                return null;

            Country? country_response_from_list = await _DbContext.Countries
                                                           .FirstOrDefaultAsync(temp => temp.CountryID == countryID);

            if (country_response_from_list == null)
                return null;

            return country_response_from_list.ToCountryResponse();
        }
    }
}