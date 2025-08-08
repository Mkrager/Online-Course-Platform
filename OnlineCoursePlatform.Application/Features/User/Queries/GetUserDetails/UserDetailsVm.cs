using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList;
using OnlineCoursePlatform.Application.Features.Enrollments.Queries.GetEnrollmentsByStudent;

namespace OnlineCoursePlatform.Application.Features.User.Queries.GetUserDetails
{
    public class UserDetailsVm
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<CourseListVm> Courses { get; set; } = default!;
        public List<StudentEnrollmentsListVm> Enrollments { get; set; } = default!;
        public List<string> Roles { get; set; } = default!;
    }
}
