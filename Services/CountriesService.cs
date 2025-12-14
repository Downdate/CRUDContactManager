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

        //constructor
        public CountriesService(bool initialize = true)
        {
            _countries = new List<Country>();
            if (initialize == true)
            {
                List<CountryAddRequest> countriesList = new List<CountryAddRequest>()
            {
                // 50 HARD-CODED COUNTRIES
                new() { CountryName = "Argentina" },
                new() { CountryName = "Australia" },
                new() { CountryName = "Austria" },
                new() { CountryName = "Belgium" },
                new() { CountryName = "Brazil" },
                new() { CountryName = "Bulgaria" },
                new() { CountryName = "Canada" },
                new() { CountryName = "Chile" },
                new() { CountryName = "China" },
                new() { CountryName = "Colombia" },
                new() { CountryName = "Croatia" },
                new() { CountryName = "Czech Republic" },
                new() { CountryName = "Denmark" },
                new() { CountryName = "Egypt" },
                new() { CountryName = "Estonia" },
                new() { CountryName = "Finland" },
                new() { CountryName = "France" },
                new() { CountryName = "Germany" },
                new() { CountryName = "Greece" },
                new() { CountryName = "Hungary" },
                new() { CountryName = "Iceland" },
                new() { CountryName = "India" },
                new() { CountryName = "Indonesia" },
                new() { CountryName = "Iran" },
                new() { CountryName = "Iraq" },
                new() { CountryName = "Ireland" },
                new() { CountryName = "Israel" },
                new() { CountryName = "Italy" },
                new() { CountryName = "Japan" },
                new() { CountryName = "Kenya" },
                new() { CountryName = "Mexico" },
                new() { CountryName = "Morocco" },
                new() { CountryName = "Netherlands" },
                new() { CountryName = "New Zealand" },
                new() { CountryName = "Nigeria" },
                new() { CountryName = "Norway" },
                new() { CountryName = "Pakistan" },
                new() { CountryName = "Peru" },
                new() { CountryName = "Philippines" },
                new() { CountryName = "Poland" },
                new() { CountryName = "Portugal" },
                new() { CountryName = "Romania" },
                new() { CountryName = "Russia" },
                new() { CountryName = "Saudi Arabia" },
                new() { CountryName = "Serbia" },
                new() { CountryName = "South Korea" },
                new() { CountryName = "Spain" },
                new() { CountryName = "Sweden" },
                new() { CountryName = "Switzerland" },
                new() { CountryName = "USA" }
            };
                List<CountryResponse> countries = new List<CountryResponse>();

                foreach (CountryAddRequest countryAddRequest in countriesList)
                {
                    countries.Add(AddCountry(countryAddRequest));
                }
            }
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