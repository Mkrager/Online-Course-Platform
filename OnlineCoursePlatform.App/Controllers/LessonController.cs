using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;

namespace OnlineCoursePlatform.App.Controllers
{
    public class LessonController : Controller
    {
        private readonly ILessonDataService _lessonDataService;

        public LessonController(ILessonDataService lessonDataService)
        {
            _lessonDataService = lessonDataService;
        }
        public async Task<IActionResult> CourseOverview(Guid courseId)
        {
            var courseLessons = await _lessonDataService.GetCourseLessons(courseId);
            return View(courseLessons);
        }
    }
}
