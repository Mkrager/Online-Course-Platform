using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList;

namespace OnlineCoursePlatform.Application.Features.Courses.Queries.GetCourseDetail
{
    public class CourseDetailVm
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public string ThumbnailUrl { get; set; } = string.Empty;
        public string LevelId { get; set; } = string.Empty;

        public CategoryDto Category { get; set; } = default!;
        public LevelDto Level { get; set; } = default!;
    }
}
