using Microsoft.AspNetCore.Mvc;

namespace CRUDContactManager.Controllers
{
    public class HomeController : Controller
    {
        [Route("Error")]
        public IActionResult Error()
        {
            return View(); // /Views/Shared/Error.cshtml
        }
    }
}
