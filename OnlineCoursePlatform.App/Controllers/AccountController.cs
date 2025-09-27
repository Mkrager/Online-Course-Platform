using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;

namespace OnlineCoursePlatform.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserDataService _userDataService;

        public AccountController(IUserDataService userDataService)
        {
            _userDataService = userDataService;
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

                viewModel = response.Data;
            }

            return View("Profile", viewModel);
        }
    }
}