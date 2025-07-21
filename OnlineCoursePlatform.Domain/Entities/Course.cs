using OnlineCoursePlatform.Domain.Common;

namespace OnlineCoursePlatform.Domain.Entities
{
    public class Course : AuditableEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public Guid LevelId { get; set; }
        public decimal Price { get; set; }
        public bool IsPublished { get; set; }
        public string ThumbnailUrl { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; }

        public Category Category { get; set; } = default!;
        public Level Level { get; set; } = default!;
        public ICollection<Lesson>? Lessons { get; set; }
    }
}
