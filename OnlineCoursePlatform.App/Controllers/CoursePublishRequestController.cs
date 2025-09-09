using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Middlewares;
using OnlineCoursePlatform.App.Services;
using OnlineCoursePlatform.App.ViewModels.Course;
using OnlineCoursePlatform.App.ViewModels.CoursePublishRequest;

namespace OnlineCoursePlatform.App.Controllers
{
    public class CoursePublishRequestController : Controller
    {
        private readonly ICoursePublishRequestDataService _coursePublishRequestDataService;

        public CoursePublishRequestController(ICoursePublishRequestDataService coursePublishRequestDataService)
        {
            _coursePublishRequestDataService = coursePublishRequestDataService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CoursePublishRequestListViewModel coursePublishRequestViewModel)
        {
            var newCourse = await _coursePublishRequestDataService.CreateCourseRequest(coursePublishRequestViewModel);

            if (newCourse.IsSuccess)
            {
                return RedirectToAction("Profile", "Account");
            }

            TempData["Message"] = HandleErrors.HandleResponse(newCourse);

            return View();
        }
    }
}
