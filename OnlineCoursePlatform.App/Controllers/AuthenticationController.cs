using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Services;
using OnlineCoursePlatform.App.ViewModels;

namespace OnlineCoursePlatform.App.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<IActionResult> Login(AuthenticateRequest request)
        {
            var result = await _authenticationService.Authenticate(request);
            TempData["Message"] = HandleResponse<bool>(result, "Success");

            if (result.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = result.ErrorText ?? "Invalid login attempt.";
            TempData["ShowLoginPopup"] = true;

            return RedirectToAction("Index", "Home");
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
