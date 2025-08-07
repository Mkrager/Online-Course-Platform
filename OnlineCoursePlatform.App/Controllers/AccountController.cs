using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels.User;
using System.Security.Claims;
using System.Text.Json;

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
        public async Task<IActionResult> Profile()
        {
            var roles = User.Claims
                            .Where(c => c.Type == ClaimTypes.Role)
                            .Select(c => c.Value)
                            .ToList();

            object viewModel;

            if (roles.Contains("Teacher"))
            {
                viewModel = await _userDataService.GetTeacherDetailsAsync();
            }
            else
            {
                viewModel = await _userDataService.GetBasicUser();
            }

            return View("Profile", viewModel);
        }
    }
}
