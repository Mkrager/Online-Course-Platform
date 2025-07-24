using OnlineCoursePlatform.Domain.Common;
using OnlineCoursePlatform.Domain.Common.Interfaces;

namespace OnlineCoursePlatform.Domain.Entities
{
    public class TestAttempt : BaseEntity, IHasUserId
    {
        public string UserId { get; set; } = string.Empty;
        public Guid TestId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsCompleted { get; set; }

        public Test Test { get; set; } = default!;
        public ICollection<UserAnswer> UserAnswers { get; set; } = default!;
    }
}
