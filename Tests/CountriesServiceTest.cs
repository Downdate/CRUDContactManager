using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Text;
using Entities;

using ServiceContracts;

using ServiceContracts.DTO;
using Services;

namespace Tests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _CountriesService;

        public CountriesServiceTest()
        {
            _CountriesService = new CountriesService(false);
        }

        #region AddCountry

        //When CountryAddRequest is null, it should throw ArgumentNullExeption
        [Fact]
        public void AddCountry_NullCountry()
        {
            //Arrange
            CountryAddRequest? request = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _CountriesService.AddCountry(request);
            });
        }

        //When the CountryName is null, it should throw ArgumentException

        [Fact]
        public void AddCountry_NullCountryName()
        {
            //Arrange
            CountryAddRequest? request = new CountryAddRequest()
            {
                CountryName = null
            };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _CountriesService.AddCountry(request);
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
        public void AddCountry_ProperCountryDetails()
        {
            //Arrange
            CountryAddRequest? request = new CountryAddRequest()
            {
                CountryName = "Japan"
            };

            //Act
            CountryResponse response = _CountriesService.AddCountry(request);
            List<CountryResponse> country_list_from_getAllCountries = _CountriesService.GetAllCountries();

            //Assert
            Assert.True(response.CountryID != Guid.Empty);
            Assert.Contains(response, country_list_from_getAllCountries);
        }

        #endregion AddCountry

        #region GetAllCountries

        //the List should be empty by default (with no adding first).
        [Fact]
        public void GetAllCountries_EmptyList()
        {
            //Acts
            List<CountryResponse> actual_countries_from_response_list = _CountriesService.GetAllCountries();
            //Assert
            Assert.Empty(actual_countries_from_response_list);
        }

        // Adding multiple countries should work fine
        [Fact]
        public void GetAllCountries_AddFewCountries()
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
                county_list_from_add_countries.Add(_CountriesService.AddCountry(countryAddRequest));
            }

            List<CountryResponse> country_list_from_getAllCountries = _CountriesService.GetAllCountries();
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
        public void GetCountryByCountryID_ExistingID()
        {
            //Arrange
            CountryAddRequest? request = new CountryAddRequest()
            {
                CountryName = "India"
            };
            CountryResponse added_country = _CountriesService.AddCountry(request);
            //Act
            CountryResponse? retrieved_country = _CountriesService.GetCountryByCountryID(added_country.CountryID);
            //Assert
            Assert.NotNull(retrieved_country);
            Assert.Equal(added_country, retrieved_country);
        }

        [Fact]
        public void GetCountryByCountryID_NullCountryID()
        {
            //Arrange
            Guid? countryID = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                CountryResponse? retrieved_country = _CountriesService.GetCountryByCountryID(countryID);
            });
        }

        [Fact]
        public void GetCountryByCountryID_NonExistingID()
        {
            //Arrange
            Guid countryID = Guid.NewGuid();
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                CountryResponse? retrieved_country = _CountriesService.GetCountryByCountryID(countryID);
            });
        }

        #endregion GetCountryByID
    }
}