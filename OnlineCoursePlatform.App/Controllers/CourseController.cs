using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Middlewares;
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

        [HttpPost]
        public async Task<IActionResult> Create(CourseDetailViewModel courseDetailViewModel)
        {
            var newCourse = await _courseDataService.CreateCourse(courseDetailViewModel);

            if (newCourse.IsSuccess)
            {
                return RedirectToAction("Overview", "Account", new { userId = User.FindFirst("uid")?.Value });
            }

            TempData["Message"] = HandleErrors.HandleResponse<Guid>(newCourse);
            TempData["Categories"] = await Categories();
            TempData["Levels"] = await Levels();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var courseToUpdate = await _courseDataService.GetCourseById(id);

            TempData["Categories"] = await Categories();
            TempData["Levels"] = await Levels();

            return View(courseToUpdate);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CourseDetailViewModel courseDetailViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray());
                return Json(new { errors });
            }

            var result = await _courseDataService.UpdateCourse(courseDetailViewModel);

            if (result.IsSuccess)
            {
                return Json(new { redirectToUrl = Url.Action("Overview", "Account", new { userId = User.FindFirst("uid")?.Value }) });
            }

            TempData["Message"] = HandleErrors.HandleResponse(result);

            return Json(new { redirectToUrl = Url.Action("Update", "Course", new { id = courseDetailViewModel.Id }) });
        }

        [HttpGet]
        public async Task<IActionResult> CategoryCourses(Guid categoryId)
        {
            var courses = await _courseDataService.GetCoursesByCategoryId(categoryId);
            return View(courses);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _courseDataService.DeleteCourse(id);

            var userId = User.FindFirst("uid")?.Value;

            var redirectUrl = Url.Action("Overview", "Account", new { userId = userId });

            return Json(new { redirectUrl });
        }
    }
}
