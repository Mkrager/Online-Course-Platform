using OnlineCoursePlatform.Domain.Common;

namespace OnlineCoursePlatform.Domain.Entities
{
    public class Lesson : AuditableEntity
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public int Order { get; set; }
        public TimeSpan Duration { get; set; }

        public Course Course { get; set; } = default!;
        public ICollection<Test> Tests { get; set; } = new List<Test>();
    }
}
