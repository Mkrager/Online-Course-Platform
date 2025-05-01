using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels.Course;

namespace OnlineCoursePlatform.App.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseDataService _courseDataService;
        private readonly ICategoryDataService _categoryDataService;
        private readonly ILevelDataService _levelDataService;

        public CourseController(ICourseDataService courseDataService, ICategoryDataService categoryDataService, ILevelDataService levelDataService)
        {
            _courseDataService = courseDataService;
            _categoryDataService = categoryDataService;
            _levelDataService = levelDataService;
        }

        public async Task<SelectList> Categories()
        {
            var categories = await _categoryDataService.GetAllCategories();
            var categoryList = new SelectList(categories, "Id", "Name");
            return categoryList;
        }
        public async Task<SelectList> Levels()
        {
            var levels = await _levelDataService.GetAllLevels();
            var levelList = new SelectList(levels, "Id", "Name");
            return levelList;
        }


        [HttpGet]
        public async Task<IActionResult> CoursesList()
        {
            var courses = await _courseDataService.GetAllCourses();
            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TempData["Categories"] = await Categories();
            TempData["Levels"] = await Levels();

            return View();
        }
    }
}
