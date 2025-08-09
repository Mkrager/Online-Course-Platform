using OnlineCoursePlatform.App.ViewModels.Course;
using OnlineCoursePlatform.App.ViewModels.Enrollments;

namespace OnlineCoursePlatform.App.ViewModels.User
{
    public class UserDetailsResponse
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<string> Roles { get; set; } = default!;
        public List<StudentEnrollmentsListViewModel> Enrollments { get; set; } = default!;
        public List<CourseListViewModel> Courses { get; set; } = default!;
    }
}
