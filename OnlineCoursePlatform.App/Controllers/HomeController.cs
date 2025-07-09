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
            var endAttemptViewModel = new EndTestAttemptViewModel()
            {
                AttempId = Guid.Parse("0bc48942-6e42-4bb8-9e5d-08ddbedcc619"),
                UserAnswerDto = new List<UserAnswerDto>()
                {
                    new UserAnswerDto()
                    {
                        AnswerId = Guid.Parse("b60a806c-1456-41ba-3e34-08ddb62bd7e2"),
                        QuestionId = Guid.Parse("2604c710-07a6-4df0-e031-08ddb62bd7e0")
                    },

                    new UserAnswerDto()
                    {
                        AnswerId = Guid.Parse("3aa2eaaf-c509-46bc-3e35-08ddb62bd7e2"),
                        QuestionId = Guid.Parse("2604c710-07a6-4df0-e031-08ddb62bd7e0")
                    },

                    new UserAnswerDto()
                    {
                        AnswerId = Guid.Parse("fd42dfbe-3d92-4347-3e36-08ddb62bd7e2"),
                        QuestionId = Guid.Parse("2604c710-07a6-4df0-e031-08ddb62bd7e0")
                    }
                }
            };

            await _testAttemptDataService.EndTestAttempt(endAttemptViewModel);

            return View();
        }
    }
}
