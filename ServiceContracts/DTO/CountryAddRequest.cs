using System;
using System.Collections.Generic;
using System.Text;
using Entities;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class for adding a new country
    /// </summary>
    public class CountryAddRequest
    {
        public string? CountryName { get; set; }

        public Country toCountry()
        {
            return new Country() { CountryName = CountryName };
        }
    }
}