using OnlineCoursePlatform.Domain.Common;

namespace OnlineCoursePlatform.Domain.Entities
{
    public class Question : BaseEntity
    {
        public Guid TestId { get; set; }
        public string Text { get; set; } = string.Empty;

        public Test Test { get; set; } = default!;
        public ICollection<Answer>? Answers { get; set; }
    }
}
