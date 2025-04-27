using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Services;
using OnlineCoursePlatform.App.ViewModels;

namespace OnlineCoursePlatform.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        public HomeController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        private string HandleResponse<T>(ApiResponse<T> response, string successMessage = "")
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return successMessage;
            }
            else
            {
                return response.ErrorText;
            }
        }

    }
}
