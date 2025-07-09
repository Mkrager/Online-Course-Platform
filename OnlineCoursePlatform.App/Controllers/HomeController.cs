using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels.TestAttempt;

namespace OnlineCoursePlatform.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserDataService _userDataService;

        public HomeController(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
