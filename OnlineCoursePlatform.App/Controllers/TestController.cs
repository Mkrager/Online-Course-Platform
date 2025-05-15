using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Middlewares;
using OnlineCoursePlatform.App.ViewModels.Test;

namespace OnlineCoursePlatform.App.Controllers
{
    public class TestController : Controller
    {
        private readonly ITestDataService _testDataService;

        public TestController(ITestDataService testDataService)
        {
            _testDataService = testDataService;
        }

        [HttpGet]
        public async Task<IActionResult> List(Guid lessonId)
        {
            var tests = await _testDataService.GetTestByLessonId(lessonId);

            var lessonTestViewModel = new LessonTestsViewModel()
            {
                Tests = tests,
                LessonId = lessonId
            };

            return View(lessonTestViewModel);
        }

        [HttpGet]
        public IActionResult Create(Guid lessonId)
        {
            var model = new TestViewModel()
            {
                LessonId = lessonId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TestViewModel testViewModel)
        {
            var result = await _testDataService.CreateTest(testViewModel);

            if (!result.IsSuccess)
            {
                TempData["Message"] = HandleErrors.HandleResponse(result);
                return View();
            }

            return RedirectToAction("Overview", "Account", new { userId = User.FindFirst("uid")?.Value });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _testDataService.DeleteTest(id);

            var referer = Request.Headers["Referer"].ToString();

            return Json(new { redirectUrl = referer });
        }
    }
}
