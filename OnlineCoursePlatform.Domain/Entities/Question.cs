namespace OnlineCoursePlatform.Domain.Entities
{
    public class Question
    {
        public Guid Id { get; set; }
        public Guid TestId { get; set; }
        public string Text { get; set; } = string.Empty;

        public Test Test { get; set; } = default!;
        public ICollection<Answer>? Answers { get; set; }
    }
}
