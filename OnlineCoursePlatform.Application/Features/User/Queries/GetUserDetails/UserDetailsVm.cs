namespace OnlineCoursePlatform.Application.Features.User.Queries.GetUserDetails
{
    public class UserDetailsVm
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = default!;
    }
}
