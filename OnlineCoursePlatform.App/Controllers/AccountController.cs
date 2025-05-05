using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;

namespace OnlineCoursePlatform.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserDataService _userDataService;
        private readonly ILessonDataService _lessonDataService;

        public AccountController(IUserDataService userDataService, ILessonDataService lessonDataService)
        {
            _userDataService = userDataService;
            _lessonDataService = lessonDataService;
        }

        [HttpGet]
        public async Task<IActionResult> Overview(string userId)
        {
            var user = await _userDataService.GetUserDetails(userId);
            return View(user);
        }
    }
}
