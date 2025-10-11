using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;

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
    }
}
