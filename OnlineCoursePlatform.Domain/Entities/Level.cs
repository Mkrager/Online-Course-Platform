using OnlineCoursePlatform.Domain.Common;

namespace OnlineCoursePlatform.Domain.Entities
{
    public class Level : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }

        public ICollection<Course>? Courses { get; set; }
    }
}
