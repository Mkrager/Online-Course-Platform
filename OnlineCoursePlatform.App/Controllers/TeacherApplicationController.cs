using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels.Request;
using OnlineCoursePlatform.App.ViewModels.TeacherApplication;

namespace OnlineCoursePlatform.App.Controllers
{
    public class TeacherApplicationController : Controller
    {
        private readonly ITeacherApplicationDataService _teacherApplicationDataService;

        public TeacherApplicationController(ITeacherApplicationDataService teacherApplicationDataService)
        {
            _teacherApplicationDataService = teacherApplicationDataService;
        }

        [HttpGet]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> List()
        {
            var list = await _teacherApplicationDataService.GetTeacherRequests();
            return View(list.Data);
        }

        [HttpPost]
        [Authorize(Roles = "Default")]
        public async Task<IActionResult> Create([FromBody] CreateTeacherApplicationRequest createTeacherApplicationRequest)
        {
            await _teacherApplicationDataService.CreateTeacherApplication(createTeacherApplicationRequest);
            return Ok(new { redirectToUrl = Url.Action("Profile", "Account") });
        }

        [HttpPut]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> Reject([FromBody] RejectRequestDto rejectRequestDto)
        {
            await _teacherApplicationDataService.RejectTeacherApplication(rejectRequestDto);
            return Ok(new { redirectToUrl = Url.Action("List", "TeacherApplication") });
        }

        [HttpPut]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> Approve(Guid id)
        {
            await _teacherApplicationDataService.ApproveTeacherApplication(id);
            return Ok(new { redirectToUrl = Url.Action("List", "TeacherApplication") });
        }
    }
}
