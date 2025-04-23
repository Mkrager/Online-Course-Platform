using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
using System.Diagnostics;

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
            var list = await _courseDataService.GetAllCourses();
            return View();
        }

    }
}
