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
        public async Task<IActionResult> StartTest(Guid id)
        {
            var test = await _testDataService.GetTestById(id);
            var attemptId = await _testAttemptDataService.StartTestAttempt(new StartTestAttemptViewModel()
            {
                TestId = id
            });
            return View(new TestAttemptViewModel()
            {
                AttemptId = attemptId.Data,
                TestViewModel = test
            });
        }

        [HttpPut]
        public async Task<IActionResult> EndTest([FromBody] EndTestAttemptViewModel model)
        {
            var result = await _testAttemptDataService.EndTestAttempt(model);
            var redirectUrl = Url.Action("Profile", "Account");

            return Ok(new { redirectUrl });
        }
    }
}
