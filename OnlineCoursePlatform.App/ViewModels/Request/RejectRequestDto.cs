namespace OnlineCoursePlatform.App.ViewModels.Request
{
    public class RejectRequestDto
    {
        public Guid Id { get; set; }
        public string RejectReason { get; set; } = string.Empty;
    }
}
