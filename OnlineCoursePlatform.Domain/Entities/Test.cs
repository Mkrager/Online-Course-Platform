namespace OnlineCoursePlatform.Domain.Entities
{
    public class Test
    {
        public Guid Id { get; set; }
        public Guid LessonId { get; set; }
        public string Title { get; set; } = string.Empty;

        public Lesson Lesson { get; set; } = default!;
        public ICollection<Question>? Questions { get; set; }
    }
}
