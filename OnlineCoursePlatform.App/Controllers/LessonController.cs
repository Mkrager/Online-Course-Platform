using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels.Lesson;

namespace OnlineCoursePlatform.App.Controllers
{
    public class LessonController : Controller
    {
        private readonly ILessonDataService _lessonDataService;

        public LessonController(ILessonDataService lessonDataService)
        {
            _lessonDataService = lessonDataService;
        }

        [HttpGet]
        public async Task<IActionResult> CourseOverview(Guid courseId)
        {
            var courseLessons = await _lessonDataService.GetCourseLessons(courseId);
            return View(courseLessons);
        }

        [HttpGet]
        public IActionResult Add(Guid courseId)
        {
            var model = new LessonViewModel
            {
                CourseId = courseId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(LessonViewModel lessonViewModel)
        {
            var result = await _lessonDataService.CreateLesson(lessonViewModel);

            var userId = User.FindFirst("uid")?.Value;

            return RedirectToAction("Overview", "Account", new { userId = userId });
        }
    }
}
