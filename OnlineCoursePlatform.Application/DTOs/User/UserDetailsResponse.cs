using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.DTOs.User
{
    public class UserDetailsResponse
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<Course> Courses { get; set; } = default!;
    }
}
