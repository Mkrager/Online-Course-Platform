namespace OnlineCoursePlatform.Application.Features.User.Queries
{
    public class UserCourseVm
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

        public UserCategoryDto Category { get; set; } = default!;
        public UserLevelDto Level { get; set; } = default!;
    }
}
