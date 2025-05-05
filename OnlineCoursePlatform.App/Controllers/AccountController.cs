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
        public async Task<IActionResult> Overview(string userId)
        {
            var user = await _userDataService.GetUserDetails(userId);
            return View(user);
        }
    }
}
