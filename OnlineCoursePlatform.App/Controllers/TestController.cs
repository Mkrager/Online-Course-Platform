using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Helpers;
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
        [Authorize]
        public async Task<IActionResult> List(Guid lessonId)
        {
            var response = await _testDataService.GetTestByLessonId(lessonId);

            if (!response.IsSuccess)
            {
                TempData["ErrorMessage"] = response.ErrorText;
                return RedirectToAction("Index", "Home");
            }

            var lessonTestViewModel = new LessonTestsViewModel()
            {
                Tests = response.Data,
                LessonId = lessonId
            };

            return View(lessonTestViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult Create(Guid lessonId)
        {
            var model = new TestViewModel()
            {
                LessonId = lessonId
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Create(TestViewModel testViewModel)
        {
            var result = await _testDataService.CreateTest(testViewModel);

            if (!result.IsSuccess)
            {
                TempData["Message"] = HandleErrors.HandleResponse(result);
                return View();
            }

            return RedirectToAction("List", "Test", new { lessonId = testViewModel.LessonId });
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Update(Guid id)
        {
            var testToUpdate = await _testDataService.GetTestById(id);
            return View(testToUpdate.Data);
        }

        [HttpPut]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Update(TestViewModel testViewModel)
         {
            var result = await _testDataService.UpdateTest(testViewModel);

            if (!result.IsSuccess)
            {
                TempData["Message"] = HandleErrors.HandleResponse(result);
                return Json(new { redirectToUrl = Url.Action("Update", "Test", new { id = testViewModel.Id }) });
            }

            return Json(new { redirectToUrl = Url.Action("List", "Test", new { lessonId = testViewModel.LessonId }) });
        }

        [HttpDelete]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _testDataService.DeleteTest(id);

            var referer = Request.Headers["Referer"].ToString();

            return Json(new { redirectUrl = referer });
        }
    }
}