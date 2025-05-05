using Microsoft.AspNetCore.Mvc;

namespace OnlineCoursePlatform.App.Controllers
{
    public class LessonController : Controller
    {
        public IActionResult CourseOverview()
        {
            return View();
        }
    }
}
