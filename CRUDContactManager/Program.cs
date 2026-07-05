using ServiceContracts;
using Services;
using Microsoft.EntityFrameworkCore;
using Entities;
using OfficeOpenXml;
using Serilog;
using CRUDContactManager.Filters.ActionFilters;
using CRUDContactManager;
using CRUDContactManager.Middleware;


var builder = WebApplication.CreateBuilder(args);

//Serilog
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) => 
{
    //Read Serilog configuration from built-in IConfiguration
    loggerConfiguration.ReadFrom.Configuration(context.Configuration);
    //Read Serilog configuration from services (IoC container), making it possible to access Services in Serilog
    loggerConfiguration.ReadFrom.Services(services);
});

//extension method for adding services into IoC container, defined in ConfigureServicesExtension class in StartupExtensions folder
builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();


if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseExceptionHandlingMiddleware();
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