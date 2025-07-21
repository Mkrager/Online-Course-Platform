using OnlineCoursePlatform.Domain.Common;

namespace OnlineCoursePlatform.Domain.Entities
{
    public class UserAnswer : BaseEntity
    {
        public Guid QuestionId { get; set; }
        public Guid AnswerId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public Guid TestAttemptId { get; set; }
        public bool IsCorrect { get; set; }

        public Question Question { get; set; } = default!;
        public Answer Answer { get; set; } = default!;
        public TestAttempt TestAttempt { get; set; } = default!;
    }
}
