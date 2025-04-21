using OnlineCoursePlatform.Domain.Common;

namespace OnlineCoursePlatform.Domain.Entities
{
    public class Category : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Course>? Courses { get; set; }
    }
}
