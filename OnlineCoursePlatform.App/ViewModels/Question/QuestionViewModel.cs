using OnlineCoursePlatform.App.ViewModels.Answer;

namespace OnlineCoursePlatform.App.ViewModels.Question
{
    public class QuestionViewModel
    {
        public string Text { get; set; } = string.Empty;
        public List<AnswerViewModel> Answers { get; set; } = default!;
    }
}
