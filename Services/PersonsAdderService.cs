using System;
using Entities;
using ServiceContracts.DTO;
using ServiceContracts;
using System.ComponentModel.DataAnnotations;
using Services.Helpers;
using ServiceContracts.Enums;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using OfficeOpenXml;
using Exceptions;

namespace Services
{
    public class PersonsAdderService : IPersonsAdderService
    {
        //private field
        

        private readonly ApplicationDbContext _DbContext;

        //constructor
        public PersonsAdderService(ApplicationDbContext personsDbContext)
        {
            _DbContext = personsDbContext;
           
        }

        //private  Task<PersonResponse> ConvertPersonToPersonResponse(Person person)
        //{
        //    PersonResponse personResponse = person.ToPersonResponse();
        //    personResponse.Country = person.Country?.CountryName;
        //    return personResponse;
        //}

        public async Task<PersonResponse> AddPerson(PersonAddRequest? personAddRequest)
        {
            //check if PersonAddRequest is not null
            if (personAddRequest == null)
            {
                throw new ArgumentNullException(nameof(personAddRequest));
            }

            //Model validation
            ValidationHelper.ModelValidation(personAddRequest);

            //convert personAddRequest into Person type
            Person person = personAddRequest.ToPerson();

            //generate PersonID
            person.PersonID = Guid.NewGuid();

            //add person object to persons list
            _DbContext.Persons.Add(person);
            await _DbContext.SaveChangesAsync();

            //convert the Person object into PersonResponse type
            return person.ToPersonResponse();
        }

        
    }
}