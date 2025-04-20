namespace OnlineCoursePlatform.Application.Features.Categories.Queries.GetCategoriesListWithCourses
{
    public class CategoryCourseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public string ThumbnailUrl { get; set; } = string.Empty;
    }
}
