using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace CRUDContactManager.Controllers
{
    public class PersonsController : Controller
    {
        //private fields
        private readonly IPersonsService _personsService;

        //constructor
        public PersonsController(IPersonsService personsService)
        {
            _personsService = personsService;
        }

        [Route("persons/index")]
        [Route("/")]
        public IActionResult Index(string searchBy, string searchString, string sortBy = nameof(PersonResponse.Name), SortOrderOptions sortOrder = SortOrderOptions.ASCENDING)
        {
            //Search
            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                {nameof(PersonResponse.Name) , "Person Name" },
                {nameof(PersonResponse.EmailAddress) , "Email Address" },
                {nameof(PersonResponse.DateOfBirth) , "Date of Birth" },
                {nameof(PersonResponse.CountryName), "Country" },
                {nameof(PersonResponse.Age), "Age" },
                {nameof(PersonResponse.Gender),"Gender" }
            };
            List<PersonResponse> persons = _personsService.GetFilteredPersonsList(searchBy, searchString);

            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;

            //Sorting
            ViewBag.Columns = new Dictionary<string, string>()
            {
                {nameof(PersonResponse.Name) , "Person Name" },
                {nameof(PersonResponse.EmailAddress) , "Email Address" },
                {nameof(PersonResponse.DateOfBirth) , "Date of Birth" },
                {nameof(PersonResponse.CountryName), "Country" },
                {nameof(PersonResponse.Age), "Age" },
                {nameof(PersonResponse.Gender), "Gender" },
            };
            persons = _personsService.GetSortedPersons(persons, sortBy, sortOrder);
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder;

            return View(persons);
        }
    }
}