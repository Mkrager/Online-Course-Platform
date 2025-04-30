using OnlineCoursePlatform.App.ViewModels.Category;
using OnlineCoursePlatform.App.ViewModels.Level;

namespace OnlineCoursePlatform.App.ViewModels.Course
{
    public class CourseListViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public string ThumbnailUrl { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public TimeSpan Duration { get; set; }

        public CategoryViewModel Category { get; set; } = default!;
        public LevelViewModel Level { get; set; } = default!;
    }
}
