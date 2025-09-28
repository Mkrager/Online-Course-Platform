using OnlineCoursePlatform.Domain.Common;

namespace OnlineCoursePlatform.Domain.Entities
{
    public class CoursePublishRequest : RequestEntity
    {
        public Guid CourseId { get; set; }

        public Course Course { get; set; } = default!;
    }
}