using Microsoft.AspNetCore.Mvc;

namespace NESTCOOKING_API.Presentation.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
