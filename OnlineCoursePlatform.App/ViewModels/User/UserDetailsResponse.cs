using OnlineCoursePlatform.App.ViewModels.Course;
using OnlineCoursePlatform.App.ViewModels.CoursePublishRequest;
using OnlineCoursePlatform.App.ViewModels.Enrollments;

namespace OnlineCoursePlatform.App.ViewModels.User
{
    public class UserDetailsResponse
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<string> Roles { get; set; } = default!;
        public List<CoursePublishRequestListViewModel>? CoursePublishRequests { get; set; }
        public List<StudentEnrollmentsListViewModel>? Enrollments { get; set; }
        public List<CourseListViewModel>? Courses { get; set; }
    }
}
