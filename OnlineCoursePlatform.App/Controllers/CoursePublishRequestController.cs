using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;

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
        public async Task<IActionResult> Approve(Guid id)
        {
            await _coursePublishRequestDataService.ApproveCourseRequest(id);
            return RedirectToAction("List", "CoursePublishRequest");
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var coursePublishRequests = await _coursePublishRequestDataService.GetAllCoursePublishRequests();

            return View(coursePublishRequests);
        }
    }
}