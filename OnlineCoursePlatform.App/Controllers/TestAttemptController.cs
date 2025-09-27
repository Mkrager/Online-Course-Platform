using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels.TestAttempt;

namespace OnlineCoursePlatform.App.Controllers
{
    public class TestAttemptController : Controller
    {
        private readonly ITestDataService _testDataService;
        private readonly ITestAttemptDataService _testAttemptDataService;

        public TestAttemptController(ITestDataService testDataService, ITestAttemptDataService testAttemptDataService)
        {
            _testDataService = testDataService;
            _testAttemptDataService = testAttemptDataService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> StartTest(Guid id)
        {
            var testResponse = await _testDataService.GetTestById(id);

            if (!testResponse.IsSuccess)
            {
                TempData["ErrorMessage"] = testResponse.ErrorText;
                return RedirectToAction("Index", "Home");
            }

            var testAttemptResponse = await _testAttemptDataService.StartTestAttempt(new StartTestAttemptViewModel()
            {
                TestId = id
            });

            if (!testAttemptResponse.IsSuccess)
            {
                TempData["ErrorMessage"] = testAttemptResponse.ErrorText;
                return RedirectToAction("Index", "Home");
            }

            return View(new TestAttemptViewModel()
            {
                AttemptId = testAttemptResponse.Data,
                TestViewModel = testResponse.Data
            });
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> EndTest([FromBody] EndTestAttemptViewModel model)
        {
            var result = await _testAttemptDataService.EndTestAttempt(model);
            var redirectUrl = Url.Action("Profile", "Account");

            return Ok(new { redirectUrl });
        }
    }
}
