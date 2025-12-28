using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using ServiceContracts.DTO;
using Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Tests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _CountriesService;

        public CountriesServiceTest()
        {
            _CountriesService = new CountriesService(new PersonsDbContext(new DbContextOptionsBuilder<PersonsDbContext>().Options));
        }

        #region AddCountry

        //When CountryAddRequest is null, it should throw ArgumentNullExeption
        [Fact]
        public async Task AddCountry_NullCountry()
        {
            //Arrange
            CountryAddRequest? request = null;

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                //Act
                await _CountriesService.AddCountry(request);
            });
        }

        //When the CountryName is null, it should throw ArgumentException

        [Fact]
        public async Task AddCountry_NullCountryName()
        {
            //Arrange
            CountryAddRequest? request = new CountryAddRequest()
            {
                CountryName = null
            };

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _CountriesService.AddCountry(request);
            });
        }

        //When the CountryName is duplicate, it should throw ArgumentExeption

        [Fact]
        public void AddCountry_DuplicateCountryName()
        {
            //Arrange
            CountryAddRequest? request1 = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            CountryAddRequest? request2 = new CountryAddRequest()
            {
                CountryName = "USA"
            };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _CountriesService.AddCountry(request1);
                _CountriesService.AddCountry(request2);
            });
        }

        //When CountryName is ok it should add the country and do it properly

        [Fact]
        public async Task AddCountry_ProperCountryDetails()
        {
            //Arrange
            CountryAddRequest? request = new CountryAddRequest()
            {
                CountryName = "Japan"
            };

            //Act
            CountryResponse response = await _CountriesService.AddCountry(request);
            List<CountryResponse> country_list_from_getAllCountries = await _CountriesService.GetAllCountries();

            //Assert
            Assert.True(response.CountryID != Guid.Empty);
            Assert.Contains(response, country_list_from_getAllCountries);
        }

        #endregion AddCountry

        #region GetAllCountries

        //the List should be empty by default (with no adding first).
        [Fact]
        public async Task GetAllCountries_EmptyList()
        {
            //Acts
            List<CountryResponse> actual_countries_from_response_list = await _CountriesService.GetAllCountries();
            //Assert
            Assert.Empty(actual_countries_from_response_list);
        }

        // Adding multiple countries should work fine
        [Fact]
        public async Task GetAllCountries_AddFewCountries()
        {
            //Arrange
            List<CountryAddRequest> country_request_list = new List<CountryAddRequest>()
            {
                new CountryAddRequest() { CountryName= "USA" },
                new CountryAddRequest() { CountryName= "Japan" },
                new CountryAddRequest() { CountryName= "Germany" },
            };

            //Act
            List<CountryResponse> county_list_from_add_countries = new List<CountryResponse>();
            foreach (CountryAddRequest countryAddRequest in country_request_list)
            {
                county_list_from_add_countries.Add(await _CountriesService.AddCountry(countryAddRequest));
            }

            List<CountryResponse> country_list_from_getAllCountries = await _CountriesService.GetAllCountries();
            //Read each element from county_list_from_add_countries
            foreach (CountryResponse expected_country in county_list_from_add_countries)
            {
                //Assert
                Assert.Contains(expected_country, country_list_from_getAllCountries);
            }
        }

        #endregion GetAllCountries

        #region GetCountryByID

        [Fact]
        public async Task GetCountryByCountryID_ExistingID()
        {
            //Arrange
            CountryAddRequest? request = new CountryAddRequest()
            {
                CountryName = "India"
            };
            CountryResponse added_country = await _CountriesService.AddCountry(request);
            //Act
            CountryResponse? retrieved_country = await _CountriesService.GetCountryByCountryID(added_country.CountryID);
            //Assert
            Assert.NotNull(retrieved_country);
            Assert.Equal(added_country, retrieved_country);
        }

        [Fact]
        public async Task GetCountryByCountryID_NullCountryID()
        {
            //Arrange
            Guid? countryID = null;

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                //Act
                CountryResponse? retrieved_country = await _CountriesService.GetCountryByCountryID(countryID);
            });
        }

        [Fact]
        public async Task GetCountryByCountryID_NonExistingID()
        {
            //Arrange
            Guid countryID = Guid.NewGuid();
            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                CountryResponse? retrieved_country = await _CountriesService.GetCountryByCountryID(countryID);
            });
        }

        #endregion GetCountryByID
    }
}