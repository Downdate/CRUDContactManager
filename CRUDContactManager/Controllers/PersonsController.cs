using CRUDContactManager.ViewModels.Persons;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using System.Threading.Tasks;

namespace CRUDContactManager.Controllers
{
    [Route("[Controller]")]
    public class PersonsController : Controller
    {
        //private fields
        private readonly IPersonsService _personsService;

        private readonly ICountriesService _countriesService;

        //constructor
        public PersonsController(IPersonsService personsService, ICountriesService countriesService)
        {
            _personsService = personsService;
            _countriesService = countriesService;
        }

        [Route("[Action]")]
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

        //Executed when HTTP GET /persons/create
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreatePersonViewModel model = new CreatePersonViewModel();
            model.Countries = _countriesService.GetAllCountries();

            return View(model);
        }

        //Executed when HTTP POST /persons/create
        [Route("[Action]")]
        [HttpPost]
        public IActionResult Create(CreatePersonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Countries = _countriesService.GetAllCountries();
                return View(model);
            }

            _personsService.AddPerson(model.Person);
            return RedirectToAction("Index");
        }

        //Executed when HTTP GET /persons/Update
        [Route("[action]/{personID}")]
        [HttpGet]
        public async Task<IActionResult> Update(Guid personID)
        {
            UpdatePersonViewModel model = new UpdatePersonViewModel();
            model.Countries = _countriesService.GetAllCountries();
            model.Person = _personsService.GetPersonByPersonID(personID).ToPersonUpdateRequest();

            return View(model);
        }

        //Executed when HTTP POST /persons/Update
        [Route("[Action]/{personID}")]
        [HttpPost]
        public IActionResult Update(UpdatePersonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Countries = _countriesService.GetAllCountries();
                return View(model);
            }

            _personsService.UpdatePerson(model.Person);
            return RedirectToAction("Index");
        }
    }
}