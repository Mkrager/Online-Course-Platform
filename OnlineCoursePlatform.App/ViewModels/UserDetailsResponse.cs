namespace OnlineCoursePlatform.App.ViewModels
{
    public class UserDetailsResponse
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<CourseListViewModel> Courses { get; set; } = default!;

    }
}
