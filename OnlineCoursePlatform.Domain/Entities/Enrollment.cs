using OnlineCoursePlatform.Domain.Common;

namespace OnlineCoursePlatform.Domain.Entities
{
    public class Enrollment : TimestampedEntity
    {
        public Guid CourseId { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public int Progress { get; set; }

        public Course Course { get; set; } = default!;
    }
}
