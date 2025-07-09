using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels.TestAttempt;

namespace OnlineCoursePlatform.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserDataService _userDataService;
        private readonly ITestAttemptDataService _testAttemptDataService;

        public HomeController(IUserDataService userDataService, ITestAttemptDataService testAttemptDataService)
        {
            _userDataService = userDataService;
            _testAttemptDataService = testAttemptDataService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var startAttemptViewModel = new StartTestAttemptViewModel()
            {
                TestId = Guid.Parse("bd49c323-c012-4a67-659f-08ddb62bd7df")
            };

            await _testAttemptDataService.StartTestAttempt(startAttemptViewModel);

            return View();
        }
    }
}
