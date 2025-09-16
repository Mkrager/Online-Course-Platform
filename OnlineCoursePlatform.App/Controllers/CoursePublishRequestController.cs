using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
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

        [HttpPut]
        public async Task<IActionResult> Approve(Guid id)
        {
            await _coursePublishRequestDataService.ApproveCourseRequest(id);
            return Ok(new { redirectToUrl = Url.Action("List", "CoursePublishRequest") });
        }

        [HttpPut]
        public async Task<IActionResult> Cancel(Guid id)
        {
            await _coursePublishRequestDataService.CancelCourseRequest(id);
            return Ok(new { redirectToUrl = Url.Action("List", "CoursePublishRequest") });
        }
        
        [HttpPut]
        public async Task<IActionResult> Reject([FromBody] RejectCourseRequestDto rejectCourseRequestDto)
        {
            await _coursePublishRequestDataService.RejectCourseRequest(rejectCourseRequestDto);
            return Ok(new { redirectToUrl = Url.Action("List", "CoursePublishRequest") });
        }



        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var coursePublishRequests = await _coursePublishRequestDataService.GetUserCoursePublishRequests();

            return View(coursePublishRequests);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var coursePublishRequests = await _coursePublishRequestDataService.GetAllCoursePublishRequests(null);

            return View(coursePublishRequests);
        }

        [HttpGet]
        public async Task<IActionResult> ListFiltered([FromQuery] CoursePublishStatus? status)
        {
            var coursePublishRequests = await _coursePublishRequestDataService.GetAllCoursePublishRequests(status);
            return Ok(new { data = coursePublishRequests });
        }
    }
}