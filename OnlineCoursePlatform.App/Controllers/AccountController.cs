using Microsoft.AspNetCore.Mvc;

namespace OnlineCoursePlatform.App.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Overview()
        {
            return View();
        }
    }
}
