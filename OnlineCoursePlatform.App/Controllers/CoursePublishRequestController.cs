using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels.Request;

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
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> Approve(Guid id)
        {
            await _coursePublishRequestDataService.ApproveCourseRequest(id);
            return Ok(new { redirectToUrl = Url.Action("List", "CoursePublishRequest") });
        }

        [HttpPut]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var response = await _coursePublishRequestDataService.CancelCourseRequest(id);
            
            if (!response.IsSuccess)
            { 
                TempData["ErrorMessage"] = response.ErrorText;
                return RedirectToAction("Index", "Home");
            }

            return Ok(new { redirectToUrl = Url.Action("List", "CoursePublishRequest") });
        }
        
        [HttpPut]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> Reject([FromBody] RejectRequestDto rejectCourseRequestDto)
        {
            await _coursePublishRequestDataService.RejectCourseRequest(rejectCourseRequestDto);
            return Ok(new { redirectToUrl = Url.Action("List", "CoursePublishRequest") });
        }


        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UserList()
        {
            var coursePublishRequests = await _coursePublishRequestDataService.GetUserCoursePublishRequests();

            return View(coursePublishRequests.Data);
        }

        [HttpGet]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> List()
        {
            var coursePublishRequests = await _coursePublishRequestDataService.GetAllCoursePublishRequests(null);

            return View(coursePublishRequests.Data);
        }

        [HttpGet]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> ListFiltered([FromQuery] RequestStatus? status)
        {
            var coursePublishRequests = await _coursePublishRequestDataService.GetAllCoursePublishRequests(status);
            return Ok(new { data = coursePublishRequests.Data });
        }
    }
}