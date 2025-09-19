using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;

namespace OnlineCoursePlatform.App.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryDataService _categoryDataService;

        public CategoryController(ICategoryDataService categoryDataService)
        {
            _categoryDataService = categoryDataService;
        }

        public async Task<IActionResult> List()
        {
            var categories = await _categoryDataService.GetAllCategories();
            return View(categories);
        }
    }
}
