using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels;

namespace OnlineCoursePlatform.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourseDataService _courseDataService;
        public HomeController(ICourseDataService courseDataService)
        {
            _courseDataService = courseDataService;
        }
        public async Task<IActionResult> Index()
            {
            CourseDetailViewModel viewModel = new CourseDetailViewModel()
            {
                Description = "test",
                IsPublished = true,
                Price = 1,
                ThumbnailUrl = "test",
                Title = "test",
                CategoryId = Guid.Parse("6f4c7e59-74c7-41c5-9fa7-4b75b7d9f3a3")
            };

            var createCourse = await _courseDataService.CreateCourse(viewModel);
            return View();
        }

    }
}
