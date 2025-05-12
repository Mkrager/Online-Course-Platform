using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TestViewModel testViewModel)
        {
            var result = await _testDataService.CreateTest(testViewModel);

            return View();
        }
    }
}
