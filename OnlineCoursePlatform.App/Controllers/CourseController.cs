using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;

namespace OnlineCoursePlatform.App.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseDataService _courseDataService;

        public CourseController(ICourseDataService courseDataService)
        {
            _courseDataService = courseDataService;
        }

        public async Task<IActionResult> CoursesList()
        {
            var courses = await _courseDataService.GetAllCourses();
            return View(courses);
        }
    }
}
