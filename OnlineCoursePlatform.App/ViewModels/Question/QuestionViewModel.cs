using OnlineCoursePlatform.App.ViewModels.Answer;
using System.ComponentModel.DataAnnotations;

namespace OnlineCoursePlatform.App.ViewModels.Question
{
    public class QuestionViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Text required")]
        public string Text { get; set; } = string.Empty;
        public List<AnswerViewModel> Answers { get; set; } = default!;
    }
}
