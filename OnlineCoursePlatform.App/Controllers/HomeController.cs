using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels;

namespace OnlineCoursePlatform.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourseDataService _courseDataService;
        public HomeController(ICourseDataService courseDataService)
        {
            _courseDataService = courseDataService;
        }
        public async Task<IActionResult> Index()
        {
            var createCourse = await _courseDataService.DeleteCourse(Guid.Parse("9d528c62-ece4-4aeb-8014-08dd82735b67"));
            return View();
        }

    }
}
