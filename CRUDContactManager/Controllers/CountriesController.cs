using Microsoft.AspNetCore.Mvc;

namespace CRUDContactManager.Controllers
{
    [Route("[controller]")]
    public class CountriesController : Controller
    {
        [Route("[action]")]
        public IActionResult UploadFromExcel()
        {
            return View();
        }
    }
}