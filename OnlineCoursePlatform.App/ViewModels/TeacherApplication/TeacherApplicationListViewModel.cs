using OnlineCoursePlatform.App.ViewModels.Request;

namespace OnlineCoursePlatform.App.ViewModels.TeacherApplication
{
    public class TeacherApplicationListViewModel
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string RequestedBy { get; set; } = string.Empty;
        public DateTime RequestedDate { get; set; }
        public string RequestedName { get; set; } = string.Empty;
        public string ProcessedBy { get; set; } = string.Empty;
        public string ProcessedName { get; set; } = string.Empty;
        public DateTime ProcessedAt { get; set; }
        public string? RejectReason { get; set; }
        public RequestStatus Status { get; set; }
        public string Bio { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;
    }
}
