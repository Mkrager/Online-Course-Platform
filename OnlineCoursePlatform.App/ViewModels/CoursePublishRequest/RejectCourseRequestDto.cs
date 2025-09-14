namespace OnlineCoursePlatform.App.ViewModels.CoursePublishRequest
{
    public class RejectCourseRequestDto
    {
        public Guid Id { get; set; }
        public string RejectReason { get; set; } = string.Empty;
    }
}
