namespace OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesByUser
{
    public class CourseByTeacherVm
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public Guid LevelId { get; set; }
        public decimal Price { get; set; }
        public bool IsPublished { get; set; }
        public string ThumbnailUrl { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; }

        public TeacherCategoryDto Category { get; set; } = default!;
        public TeacherLevelDto Level { get; set; } = default!;
    }
}
