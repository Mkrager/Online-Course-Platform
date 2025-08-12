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
        public async Task<IActionResult> Profile()
        {
            object viewModel;

            if(User.FindFirst("roles").Value == "Teacher")
            {
                viewModel = await _userDataService.GetTeacherDetailsAsync();
            }
            else
            {
                viewModel = await _userDataService.GetDefaultUserDetailsAsync();
            }

            //if(User.IsInRole("Teacher"))
            //{
            //    viewModel = await _userDataService.GetTeacherDetailsAsync();
            //}
            //else
            //{
            //    viewModel = await _userDataService.GetDefaultUserDetailsAsync();
            //}

            return View("Profile", viewModel);
        }
    }
}
