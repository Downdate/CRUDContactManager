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
    public class PersonsDeleterService : IPersonsDeleterService
    {
        //private field
        

        private readonly ApplicationDbContext _DbContext;

        //constructor
        public PersonsDeleterService(ApplicationDbContext personsDbContext)
        {
            _DbContext = personsDbContext;
           
        }

        
        public async Task<bool> DeletePerson(Guid? personID)
        {
            var rows = await _DbContext.Persons
                .Where(p => p.PersonID == personID)
                .ExecuteDeleteAsync();

            return rows > 0;
        }

        
    }
}