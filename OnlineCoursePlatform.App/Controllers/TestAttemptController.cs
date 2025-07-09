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

        public async Task<IActionResult> Pass(Guid id)
        {
            var test = await _testDataService.GetTestById(id);
            await _testAttemptDataService.StartTestAttempt(new StartTestAttemptViewModel()
            {
                TestId = id
            });
            return View(test);
        }
    }
}
