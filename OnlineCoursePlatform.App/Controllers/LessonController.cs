using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Middlewares;
using OnlineCoursePlatform.App.ViewModels.Course;
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
        public async Task<IActionResult> Details(Guid id)
        {
            var lesson = await _lessonDataService.GetLessonById(id);
            return View(lesson);
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

            if (result.IsSuccess)
            {
                return RedirectToAction("CourseOverview", "Lesson", new { courseId = lessonViewModel.CourseId });
            }

            TempData["Message"] = HandleErrors.HandleResponse(result, "Success");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var lessonToUpdate = await _lessonDataService.GetLessonById(id);
            return View(lessonToUpdate);
        }

        [HttpPut]
        public async Task<IActionResult> Update(LessonViewModel lessonViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray());
                return Json(new { errors });
            }

            var result = await _lessonDataService.UpdateLesson(lessonViewModel);

            if (result.IsSuccess)
            {
                return Json(new { redirectToUrl = Url.Action("CourseOverview", "Lesson", new { courseId = lessonViewModel.CourseId }) });
            }

            TempData["Message"] = HandleErrors.HandleResponse(result);

            return Json(new { redirectToUrl = Url.Action("Update", "Lesson", new { id = lessonViewModel.Id }) });

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
