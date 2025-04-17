namespace OnlineCoursePlatform.Domain.Entities
{
    public class Test
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;

        public Course Course { get; set; } = default!;
        public ICollection<Question>? Questions { get; set; }
    }
}
