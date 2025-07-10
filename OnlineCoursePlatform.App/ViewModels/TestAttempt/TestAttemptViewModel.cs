using OnlineCoursePlatform.App.ViewModels.Test;

namespace OnlineCoursePlatform.App.ViewModels.TestAttempt
{
    public class TestAttemptViewModel
    {
        public TestViewModel TestViewModel { get; set; } = default!;
        public Guid AttemptId { get; set; }
    }
}
