using ServiceContracts;
using Services;
using Microsoft.EntityFrameworkCore;
using Entities;
using OfficeOpenXml;
using Serilog;
using CRUDContactManager.Filters.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

//Serilog
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) => 
{
    //Read Serilog configuration from built-in IConfiguration
    loggerConfiguration.ReadFrom.Configuration(context.Configuration);
    //Read Serilog configuration from services (IoC container), making it possible to access Services in Serilog
    loggerConfiguration.ReadFrom.Services(services);
});

//it adds filters globally to all action methods of all controllers
builder.Services.AddControllersWithViews(options =>
{
    var logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<ResponseHeaderActionFilter>>();
    options.Filters.Add(new ResponseHeaderActionFilter(logger, "X-Global-Custom-Key", "Global-Custom-Value"));
    
});


builder.Services.AddControllersWithViews();

//add services into IoC container
builder.Services.AddScoped<ICountriesService, CountriesService>();
builder.Services.AddScoped<IPersonsService, PersonsService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

ExcelPackage.License.SetNonCommercialPersonal("Downdate");

//Adding Http Logging to builder
builder.Services.AddHttpLogging();

var app = builder.Build();


if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
//Http Logging
app.UseHttpLogging();

//app.Logger.LogDebug("debug-message");
//app.Logger.LogInformation("Information-message");
//app.Logger.LogWarning("warning-message");
//app.Logger.LogError("error-message");
//app.Logger.LogCritical("critical-message");

Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();