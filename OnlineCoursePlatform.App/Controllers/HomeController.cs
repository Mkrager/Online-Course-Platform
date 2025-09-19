using Microsoft.AspNetCore.Mvc;

namespace OnlineCoursePlatform.App.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
