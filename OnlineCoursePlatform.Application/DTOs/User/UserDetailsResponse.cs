using OnlineCoursePlatform.Application.Features.User.Queries.GetUserDetails;

namespace OnlineCoursePlatform.Application.DTOs.User
{
    public class UserDetailsResponse
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<string> Roles { get; set; } = default!;
        public List<UserCourseVm> Courses { get; set; } = default!;
    }
}
