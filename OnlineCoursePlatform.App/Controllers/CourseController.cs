using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Middlewares;
using OnlineCoursePlatform.App.ViewModels.Course;
using OnlineCoursePlatform.App.ViewModels.CoursePublishRequest;
using System.Runtime.CompilerServices;

namespace OnlineCoursePlatform.App.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseDataService _courseDataService;
        private readonly ICategoryDataService _categoryDataService;
        private readonly ILevelDataService _levelDataService;
        private readonly ICoursePublishRequestDataService _coursePublishRequestDataService
        public CourseController(
            ICourseDataService courseDataService, 
            ICategoryDataService categoryDataService, 
            ILevelDataService levelDataService,
            ICoursePublishRequestDataService coursePublishRequestDataService)
        {
            _courseDataService = courseDataService;
            _categoryDataService = categoryDataService;
            _levelDataService = levelDataService;
            _coursePublishRequestDataService = coursePublishRequestDataService;
        }

        private async Task<SelectList> Categories()
        {
            var categories = await _categoryDataService.GetAllCategories();
            var categoryList = new SelectList(categories, "Id", "Name");
            return categoryList;
        }
        private async Task<SelectList> Levels()
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
                await _coursePublishRequestDataService.CreateCourseRequest(new CoursePublishRequestListViewModel()
                {
                    CourseId = newCourse.Data
                });

                return RedirectToAction("Profile", "Account");
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
                return Json(new { redirectToUrl = Url.Action("Profile", "Account") });
            }

            TempData["Message"] = HandleErrors.HandleResponse(result);

            return Json(new { redirectToUrl = Url.Action("Update", "Course") });
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

            return Json(new { redirectUrl = Url.Action("Profile", "Account") });
        }
    }
}
