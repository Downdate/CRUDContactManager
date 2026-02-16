using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDContactManager.Filters.ActionFilters
{
    public class PersonsListActionFilter : IActionFilter
    {
        private readonly ILogger<PersonsListActionFilter> _logger;

        public PersonsListActionFilter(ILogger<PersonsListActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // To do: add after action execution logic here
            _logger.LogInformation("PersonsListActionFilter: OnActionExecuted method is executing");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // To do: add before action execution logic here 
            _logger.LogInformation("PersonsListActionFilter: OnActionExecuting method is executing");
        }
    }
}
