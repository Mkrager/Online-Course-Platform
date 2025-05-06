namespace OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList
{
    public class CourseListVm
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public Guid LevelId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string? CreatedBy { get; set; }

        public CategoryDto Category { get; set; } = default!;
        public LevelDto Level { get; set; } = default!;
    }
}
