using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Features.TeacherApplications.Queries.GetTeacherApplicationLIst
{
    public class TeacherApplicationListVm
    {
        public Guid Id { get; set; }
        public string RequestedBy { get; set; } = string.Empty;
        public string RequestedName { get; set; } = string.Empty;
        public DateTime RequestedDate { get; set; }
        public string ProcessedBy { get; set; } = string.Empty;
        public string ProcessedName { get; set; } = string.Empty;
        public DateTime ProcessedAt { get; set; }
        public string? RejectReason { get; set; }
        public RequestStatus Status { get; set; }
    }
}
