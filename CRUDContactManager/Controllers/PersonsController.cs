using CRUDContactManager.ViewModels.Persons;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
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
        public async Task<IActionResult> Index(string searchBy, string searchString, string sortBy = nameof(PersonResponse.PersonName), SortOrderOptions sortOrder = SortOrderOptions.ASC)
        {
            //Search
            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                {nameof(PersonResponse.PersonName) , "Person Name" },
                {nameof(PersonResponse.Email) , "Email Address" },
                {nameof(PersonResponse.DateOfBirth) , "Date of Birth" },
                {nameof(PersonResponse.Country), "Country" },
                {nameof(PersonResponse.Age), "Age" },
                {nameof(PersonResponse.Gender),"Gender" }
            };
            List<PersonResponse> persons = await _personsService.GetFilteredPersons(searchBy, searchString);

            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;

            //Sorting
            ViewBag.Columns = new Dictionary<string, string>()
            {
                {nameof(PersonResponse.PersonName) , "Person Name" },
                {nameof(PersonResponse.Email) , "Email Address" },
                {nameof(PersonResponse.DateOfBirth) , "Date of Birth" },
                {nameof(PersonResponse.Country), "Country" },
                {nameof(PersonResponse.Age), "Age" },
                {nameof(PersonResponse.Gender), "Gender" },
            };
            persons = await _personsService.GetSortedPersons(persons, sortBy, sortOrder);
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
            model.Countries = await _countriesService.GetAllCountries();

            return View(model);
        }

        //Executed when HTTP POST /persons/create
        [Route("[Action]")]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePersonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Countries = await _countriesService.GetAllCountries();
                return View(model);
            }

            await _personsService.AddPerson(model.Person);
            return RedirectToAction("Index");
        }

        //Executed when HTTP GET /persons/Update
        [Route("[action]/{personID}")]
        [HttpGet]
        public async Task<IActionResult> Update(Guid personID)
        {
            UpdatePersonViewModel model = new UpdatePersonViewModel();
            model.Countries = await _countriesService.GetAllCountries();
            PersonResponse? personResponse = await _personsService.GetPersonByPersonID(personID);
            if (personResponse == null)
            {
                return RedirectToAction("Index");
            }
            model.Person = personResponse.ToPersonUpdateRequest();

            return View(model);
        }

        //Executed when HTTP POST /persons/Update
        [Route("[Action]/{personID}")]
        [HttpPost]
        public async Task<IActionResult> Update(UpdatePersonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Countries = await _countriesService.GetAllCountries();
                return View(model);
            }

            await _personsService.UpdatePerson(model.Person);
            return RedirectToAction("Index");
        }

        //Executed when HTTP GET /persons/Delete
        [Route("[action]/{personID}")]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid personID)
        {
            PersonResponse? personResponse = await _personsService.GetPersonByPersonID(personID);
            if (personResponse == null)
            {
                return RedirectToAction("Index");
            }
            return View(personResponse);
        }

        //Executed when HTTP POST /persons/Delete
        [Route("[action]/{personID}")]
        [HttpPost]
        public IActionResult Delete(PersonResponse model)
        {
            _personsService.DeletePerson(model.PersonID);
            return RedirectToAction("Index");
        }

        [Route("[Action]")]
        public async Task<IActionResult> PersonsPDF()
        {
            //Get List of Persons
            List<PersonResponse> persons = await _personsService.GetAllPersons();
            return new ViewAsPdf("PersonsPDF", persons, ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins(20, 20, 20, 20),
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                CustomSwitches = "--enable-local-file-access"
            };
        }

        [Route("[Action]")]
        public async Task<IActionResult> PersonsCSV()
        {
            MemoryStream memoryStream = await _personsService.GetPersonsCSV();

            return File(memoryStream, "application/octet-stream", "persons.csv");
        }

        [Route("[Action]")]
        public async Task<IActionResult> PersonsExcel()
        {
            MemoryStream memoryStream = await _personsService.GetPersonsExcel();

            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "persons.xlsx");
        }
    }
}