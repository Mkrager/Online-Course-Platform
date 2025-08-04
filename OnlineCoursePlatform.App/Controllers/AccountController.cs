using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels.User;
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

        public async Task<IActionResult> RedirectToAccount()
        {
            var user = await _userDataService.GetUserDetails();
            TempData["User"] = JsonSerializer.Serialize(user);

            if (user.Roles.Contains("Admin"))
                return RedirectToAction("Admin", "Account");

            if (user.Roles.Contains("Moderator"))
                return RedirectToAction("Moderator", "Account");

            if (user.Roles.Contains("Teacher"))
                return RedirectToAction("Teacher", "Account");

            if (user.Roles.Contains("Default"))
                return RedirectToAction("Default", "Account");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Default()
        {
            if (TempData["User"] is string userJson)
            {
                var user = JsonSerializer.Deserialize<UserDetailsResponse>(userJson);
                return View(user);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Admin()
        {
            if (TempData["User"] is string userJson)
            {
                var user = JsonSerializer.Deserialize<UserDetailsResponse>(userJson);
                return View(user);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
