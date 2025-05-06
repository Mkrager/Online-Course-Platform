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

            var model = new CourseLessonsViewModel()
            {
                Lessons = courseLessons,
                CourseId = courseId           
            };

            return View(model);
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

            return RedirectToAction("CourseOverview", "Lesson", new { courseId = lessonViewModel.CourseId });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _lessonDataService.DeleteLesson(id);

            var referer = Request.Headers["Referer"].ToString();

            return Json(new { redirectUrl = referer });
        }
    }
}
