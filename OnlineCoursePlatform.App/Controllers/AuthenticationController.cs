using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Helpers;
using OnlineCoursePlatform.App.ViewModels.Authenticate;

namespace OnlineCoursePlatform.App.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public IActionResult UnAuthorizedHandler()
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["LoginErrorMessage"] = "You need to login to continue.";
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public IActionResult AccessDeniedHandler()
        {
            TempData["ErrorMessage"] = "You don't have access";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthenticationViewModel request)
        {
            var result = await _authenticationService.Authenticate(request.AuthenticateRequest);
            TempData["LoginErrorMessage"] = HandleErrors.HandleResponse<bool>(result, "Success");

            if (result.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(AuthenticationViewModel request)
        {
            var result = await _authenticationService.Register(request.RegistrationRequest);
            TempData["LoginErrorMessage"] = HandleErrors.HandleResponse(result, "Success");

            if (result.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}