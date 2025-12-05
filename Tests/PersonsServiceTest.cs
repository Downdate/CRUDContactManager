using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class PersonsServiceTest
    {
        //private fields
        private readonly IPersonsService _PersonsService;

        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _testOutputHelper;

        //constructor

        public PersonsServiceTest(ITestOutputHelper testOutputHelper)
        {
            _PersonsService = new PersonsService();
            _countriesService = new CountriesService();
            _testOutputHelper = testOutputHelper;
        }

        private List<PersonResponse> AddSamplePersons()
        {
            CountryAddRequest countryAddRequest1 = new CountryAddRequest() { CountryName = "India" };
            CountryAddRequest countryAddRequest2 = new CountryAddRequest() { CountryName = "USA" };
            CountryResponse countryResponse1 = _countriesService.AddCountry(countryAddRequest1);
            CountryResponse countryResponse2 = _countriesService.AddCountry(countryAddRequest2);
            PersonAddRequest personAddRequest1 = new PersonAddRequest()
            {
                Name = "Raj",
                Address = "Mumbai, India",
                DateOfBirth = new DateTime(1992, 3, 10),
                CountryID = countryResponse1.CountryID,
                EmailAddress = "asd@gmail.com",
                Gender = GenderOptions.Female,
                ReceiveNewsLetters = true,
            };

            PersonAddRequest personAddRequest2 = new PersonAddRequest()
            {
                Name = "Simran",
                Address = "Delhi, India",
                DateOfBirth = new DateTime(1994, 7, 22),
                CountryID = countryResponse1.CountryID,
                EmailAddress = "asdasd@gmail.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true,
            };

            PersonAddRequest personAddRequest3 = new PersonAddRequest()
            {
                Name = "Mr.House",
                Address = "Goodsprings, USA",
                DateOfBirth = new DateTime(2020, 7, 22),
                CountryID = countryResponse2.CountryID,
                EmailAddress = "House@gmail.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true,
            };

            List<PersonAddRequest> personAddRequests = new List<PersonAddRequest>() { personAddRequest1, personAddRequest2, personAddRequest3 };

            List<PersonResponse> personResponses_fromAdditions = new List<PersonResponse>();

            foreach (PersonAddRequest personAddRequest in personAddRequests)
            {
                PersonResponse personResponse = _PersonsService.AddPerson(personAddRequest);
                personResponses_fromAdditions.Add(personResponse);
            }
            //print personResponses_fromAdditions

            _testOutputHelper.WriteLine("expected: ");

            foreach (PersonResponse par in personResponses_fromAdditions)
            {
                _testOutputHelper.WriteLine(par.ToString());
            }

            return personResponses_fromAdditions;
        }

        #region AddPerson

        //When we supply with null value as PersonAddRequest it should throw argumentNullException

        [Fact]
        public void AddPerson_NullPersonAddRequest()
        {
            //Arrange
            PersonAddRequest? personAddRequest = null;
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _PersonsService.AddPerson(personAddRequest));
        }

        // When we supply with null value as PersonName it should throw argumentException

        [Fact]
        public void AddPerson_PersonNameNull()
        {
            //Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest() { Name = null };
            //Act & Assert
            Assert.Throws<ArgumentException>(() => _PersonsService.AddPerson(personAddRequest));
        }

        [Fact]
        public void AddPerson_ProperPersonDetails()
        {
            //Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                Name = "Dan",
                Address = "123 Street, City",
                DateOfBirth = new DateTime(1990, 1, 1),
                CountryID = Guid.NewGuid(),
                EmailAddress = "asd@gmail.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true,
            };
            //Act
            PersonResponse personResponse = _PersonsService.AddPerson(personAddRequest);
            List<PersonResponse> allPersons = _PersonsService.GetPersonList();

            //Assert
            Assert.True(personResponse.ID != Guid.Empty);
            Assert.Contains(personResponse, allPersons);
        }

        #endregion AddPerson

        #region GetPersonByPersonID

        //If personID is null , it should return null

        [Fact]
        public void GetPersonByPersonID_PersonIDNull()
        {
            //Arrange
            Guid? personID = null;
            //Act
            PersonResponse? personResponse = _PersonsService.GetPersonByPersonID(personID);
            //Assert
            Assert.Null(personResponse);
        }

        //if personID is valid, should return valid person

        [Fact]
        public void GetPersonByPersonID_ValidPersonID()
        {
            //Arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest() { CountryName = "Japan" };
            CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                Name = "Akira",
                Address = "Tokyo, Japan",
                DateOfBirth = new DateTime(1985, 5, 15),
                CountryID = countryResponse.CountryID,
                EmailAddress = "Akira@gmail.com",
                Gender = GenderOptions.Female,
                ReceiveNewsLetters = true,
            };

            PersonResponse personResponseAdded = _PersonsService.AddPerson(personAddRequest);
            //Act
            PersonResponse? personResponse = _PersonsService.GetPersonByPersonID(personResponseAdded.ID);

            //Assert
            Assert.Equal(personResponseAdded, personResponse);
        }

        #endregion GetPersonByPersonID

        #region GetPersonList

        //if no persons exist, it should return empty list

        [Fact]
        public void GetPersonList_NoPersonsExist()
        {
            //Arrange
            //Act
            List<PersonResponse> allPersons = _PersonsService.GetPersonList();
            //Assert
            Assert.Empty(allPersons);
        }

        //if persons exist, it should return all persons

        [Fact]
        public void GetPersonList_PersonsExist()
        {
            //Arrange
            List<PersonResponse> personResponses_fromAdditions = AddSamplePersons();
            //Act
            List<PersonResponse> allPersons = _PersonsService.GetPersonList();

            // print allPersons

            _testOutputHelper.WriteLine("actual: ");

            foreach (PersonResponse pr in allPersons)
            {
                _testOutputHelper.WriteLine(pr.ToString());
            }

            //Assert
            foreach (PersonResponse personResponse in personResponses_fromAdditions)
            {
                Assert.Contains(personResponse, allPersons);
            }
        }

        #endregion GetPersonList

        #region GetFilteredPersonsList

        //if searchString is empty, it should return all persons

        [Fact]
        public void GetFilteredPersonsList_SearchStringIsEmpty()
        {
            //Arrange
            List<PersonResponse> personResponses_fromAdditions = AddSamplePersons();
            //Act
            List<PersonResponse> allPersons_from_Search = _PersonsService.GetFilteredPersonsList(nameof(Person.Name), "");

            // print allPersons

            _testOutputHelper.WriteLine("actual: ");

            foreach (PersonResponse pr in allPersons_from_Search)
            {
                _testOutputHelper.WriteLine(pr.ToString());
            }

            //Assert
            foreach (PersonResponse personResponse in personResponses_fromAdditions)
            {
                Assert.Contains(personResponse, allPersons_from_Search);
            }
        }

        #endregion GetFilteredPersonsList
    }
}