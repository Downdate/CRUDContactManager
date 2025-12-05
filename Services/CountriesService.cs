using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using System.Data;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        //list of countries
        private readonly List<Country> _countries;

        public CountriesService()
        {
            _countries = new List<Country>();
        }

        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            ArgumentNullException.ThrowIfNull(countryAddRequest);

            //converts CountryAddRequest to Country type
            Country country = countryAddRequest.toCountry();

            //check name for null
            if (country.CountryName == null)
            {
                throw new ArgumentException(nameof(country.CountryName));
            }

            //check list for duplicate name

            if (_countries.Where(temp => temp.CountryName == country.CountryName).Count() > 0)
            {
                throw new ArgumentException("This country name already exists in the country list");
            }
            else
            {
                //adding the new guid
                country.CountryID = Guid.NewGuid();
                //adding country to the list at _countries
                _countries.Add(country);
                return country.ToCountryResponse();
            }
        }

        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(country => country.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetCountryByCountryID(Guid? countryID)
        {
            if (countryID == null)
            {
                throw new ArgumentNullException(nameof(countryID));
            }

            List<CountryResponse> allCountries = GetAllCountries();
            foreach (CountryResponse country in allCountries)
            {
                if (country.CountryID == countryID)
                {
                    return country;
                }
            }

            throw new ArgumentException("Country with the given ID does not exist");
        }
    }
}