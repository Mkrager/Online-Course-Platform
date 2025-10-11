using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels.Request;

namespace OnlineCoursePlatform.App.Controllers
{
    public class TeacherApplicationController : Controller
    {
        private readonly ITeacherApplicationDataService _teacherApplicationDataService;

        public TeacherApplicationController(ITeacherApplicationDataService teacherApplicationDataService)
        {
            _teacherApplicationDataService = teacherApplicationDataService;
        }
        public async Task<IActionResult> List()
        {
            var list = await _teacherApplicationDataService.GetTeacherRequests();
            return View(list.Data);
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
