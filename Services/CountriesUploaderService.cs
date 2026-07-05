using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

namespace Services
{
    public class CountriesUploaderService : ICountriesUploaderService
    {
        //private field
        private readonly ApplicationDbContext _DbContext;

        //constructor
        public CountriesUploaderService(ApplicationDbContext personsDbContext)
        {
            _DbContext = personsDbContext;
        }
        public async Task<int> UploadCountriesFromExcelFile(IFormFile formFile)
        {
            MemoryStream memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            int countriesInserted = 0;

            using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets["Countries"];

                int rowCount = workSheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    string? cellValue = Convert.ToString(workSheet.Cells[row, 1].Value);

                    if (!string.IsNullOrEmpty(cellValue))
                    {
                        string? countryName = cellValue;

                        if (_DbContext.Countries.Where(temp => temp.CountryName == countryName).Count() == 0)
                        {
                            Country country = new Country() { CountryName = countryName };
                            _DbContext.Countries.Add(country);
                            await _DbContext.SaveChangesAsync();

                            countriesInserted++;
                        }
                    }
                }
            }

            return countriesInserted;
        }
    }
}