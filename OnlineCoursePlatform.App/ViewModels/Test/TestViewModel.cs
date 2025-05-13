using OnlineCoursePlatform.App.ViewModels.Question;
using System.ComponentModel.DataAnnotations;

namespace OnlineCoursePlatform.App.ViewModels.Test
{
    public class TestViewModel
    {
        public Guid LessonId { get; set; }

        [Required(ErrorMessage = "Title required")]
        public string Title { get; set; } = string.Empty;

        public List<QuestionViewModel> Questions { get; set; } = default!;
    }
}
