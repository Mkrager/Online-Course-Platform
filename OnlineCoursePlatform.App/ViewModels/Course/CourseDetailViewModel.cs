namespace OnlineCoursePlatform.App.ViewModels.Course
{
    public class CourseDetailViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public bool IsPublished { get; set; }
        public string ThumbnailUrl { get; set; } = string.Empty;
    }
}
