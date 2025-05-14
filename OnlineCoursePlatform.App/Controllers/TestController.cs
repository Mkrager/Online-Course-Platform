using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Middlewares;
using OnlineCoursePlatform.App.ViewModels.Lesson;
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
    }
}
