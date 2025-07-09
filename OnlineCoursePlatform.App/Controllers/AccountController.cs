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

        //Pererobiti shob userId bravsya s beku
        [HttpGet]
        public async Task<IActionResult> Overview()
        {
            var user = await _userDataService.GetUserDetails();
            return View(user);
        }
    }
}
