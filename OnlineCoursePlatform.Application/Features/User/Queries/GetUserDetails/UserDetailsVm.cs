using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.User.Queries.GetUserDetails
{
    public class UserDetailsVm
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<CourseListVm> Courses { get; set; } = default!;
        public List<string> Roles { get; set; } = default!;
    }
}
