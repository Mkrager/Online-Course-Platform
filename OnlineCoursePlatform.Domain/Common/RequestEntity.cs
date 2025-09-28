using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Domain.Common
{
    public abstract class RequestEntity : AuditableEntity
    {
        public string RequestedBy { get; set; } = string.Empty;
        public string? RejectReason { get; set; }
        public DateTime RequestedDate { get; set; }
        public string ProcessedBy { get; set; } = string.Empty;
        public DateTime ProcessedAt { get; set; }
        public RequestStatus Status { get; set; }
    }
}