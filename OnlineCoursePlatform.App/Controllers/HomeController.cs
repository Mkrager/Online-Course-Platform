using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels.Answer;
using OnlineCoursePlatform.App.ViewModels.Question;
using OnlineCoursePlatform.App.ViewModels.Test;

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
