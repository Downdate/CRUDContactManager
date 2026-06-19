using CRUDContactManager.Filters.ActionFilters;
using Entities;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using ServiceContracts;
using Services;

namespace CRUDContactManager
{
    public static class ConfigureServicesExtension
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //it adds filters globally to all action methods of all controllers
            services.AddControllersWithViews(options =>
            {
                var logger = services.BuildServiceProvider().GetRequiredService<ILogger<ResponseHeaderActionFilter>>();
                options.Filters.Add(new ResponseHeaderActionFilter(logger, "X-Global-Custom-Key", "Global-Custom-Value"));

            });


            services.AddControllersWithViews();

            //add services into IoC container
            services.AddScoped<ICountriesService, CountriesService>();
            services.AddScoped<IPersonsService, PersonsService>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            ExcelPackage.License.SetNonCommercialPersonal("Downdate");

            //Adding Http Logging to builder
            services.AddHttpLogging();
        }
    }
}
