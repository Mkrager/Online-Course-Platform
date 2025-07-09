namespace OnlineCoursePlatform.App.ViewModels.TestAttempt
{
    public class EndTestAttemptViewModel
    {
        public Guid AttempId { get; set; }
        public List<UserAnswerDto> UserAnswerDto { get; set; } = default!;
    }
}
