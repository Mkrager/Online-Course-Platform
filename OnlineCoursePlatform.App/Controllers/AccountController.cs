using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels.Request;

namespace OnlineCoursePlatform.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserDataService _userDataService;
        private readonly ITeacherApplicationDataService _teacherApplicationDataService;
        public AccountController(IUserDataService userDataService, ITeacherApplicationDataService teacherApplicationDataService)
        {
            _userDataService = userDataService;
            _teacherApplicationDataService = teacherApplicationDataService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            object viewModel;

            if (User.IsInRole("Teacher"))
            {
                var response = await _userDataService.GetTeacherDetailsAsync();

                if (!response.IsSuccess || response.Data == null)
                {
                    TempData["ErrorMessage"] = response.ErrorText;
                    return RedirectToAction("Index", "Home");
                }

                viewModel = response.Data;
            }
            else
            {
                var response = await _userDataService.GetDefaultUserDetailsAsync();

                if (!response.IsSuccess || response.Data == null)
                {
                    TempData["ErrorMessage"] = response.ErrorText;
                    return RedirectToAction("Index", "Home");
                }

                var pendingTeacherApplication = (await _teacherApplicationDataService.GetUserTeacherRequests()).Data.Where(r => r.Status == RequestStatus.Pending).ToList();

                response.Data.TeacherApplications = pendingTeacherApplication;
                viewModel = response.Data;
            }

            return View("Profile", viewModel);
        }
    }
}