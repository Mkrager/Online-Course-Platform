using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Middlewares;
using OnlineCoursePlatform.App.Services;
namespace OnlineCoursePlatform.App.Controllers
{
    public class CoursePublishRequestController : Controller
    {
        private readonly ICoursePublishRequestDataService _coursePublishRequestDataService;

        public CoursePublishRequestController(ICoursePublishRequestDataService coursePublishRequestDataService)
        {
            _coursePublishRequestDataService = coursePublishRequestDataService;
        }
    }
}
