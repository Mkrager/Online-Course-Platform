using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineCoursePlatform.App.Contracts;

namespace OnlineCoursePlatform.App.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseDataService _courseDataService;
        private readonly ICategoryDataService _categoryDataService;

        public CourseController(ICourseDataService courseDataService, ICategoryDataService categoryDataService)
        {
            _courseDataService = courseDataService;
            _categoryDataService = categoryDataService;
        }

        public async Task<SelectList> Categories()
        {
            var categories = await _categoryDataService.GetAllCategories();
            var categoryList = new SelectList(categories, "Id", "Name");
            return categoryList;
        }
        //public async Task<SelectList> Levels()
        //{
        //    var levels = await _categoryDataService.GetAllCategories();
        //    var categoryList = new SelectList(categories, "Id", "Name");
        //    return categoryList;
        //}


        [HttpGet]
        public async Task<IActionResult> CoursesList()
        {
            var courses = await _courseDataService.GetAllCourses();
            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = _categoryDataService.GetAllCategories();


            return View();
        }
    }
}
