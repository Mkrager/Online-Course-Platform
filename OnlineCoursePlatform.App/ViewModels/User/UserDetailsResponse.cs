using OnlineCoursePlatform.App.ViewModels.Course;

namespace OnlineCoursePlatform.App.ViewModels.User
{
    public class UserDetailsResponse
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<string> Roles { get; set; } = default!;
        public List<CourseListViewModel> Courses { get; set; } = default!;

    }
}
