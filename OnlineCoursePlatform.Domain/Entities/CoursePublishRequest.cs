using OnlineCoursePlatform.Domain.Common;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Domain.Entities
{
    public class CoursePublishRequest : AuditableEntity
    {
        public Guid CourseId { get; set; }
        public string RequestedBy { get; set; } = string.Empty;
        public string? RejectReason { get; set; }
        public DateTime RequestedDate { get; set; }
        public string ProcessedBy { get; set; } = string.Empty;
        public DateTime ProcessedAt { get; set; }
        public CoursePublishStatus Status { get; set; }

        public Course Course { get; set; } = default!;
    }
}
