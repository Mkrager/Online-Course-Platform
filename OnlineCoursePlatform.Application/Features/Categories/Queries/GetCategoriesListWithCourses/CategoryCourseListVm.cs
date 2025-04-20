namespace OnlineCoursePlatform.Application.Features.Categories.Queries.GetCategoriesListWithCourses
{
    public class CategoryCourseListVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<CategoryCourseDto>? Courses { get; set; }
    }
}
