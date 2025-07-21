using OnlineCoursePlatform.Domain.Common;

namespace OnlineCoursePlatform.Domain.Entities
{
    public class Enrollment : BaseEntity
    {
        public Guid CourseId { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public DateTime EnrolledAt { get; set; }
        public int Progress { get; set; }

        public Course Course { get; set; } = default!;
    }
}
