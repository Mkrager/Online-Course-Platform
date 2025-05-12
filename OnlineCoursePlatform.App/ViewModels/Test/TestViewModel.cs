using OnlineCoursePlatform.App.ViewModels.Question;

namespace OnlineCoursePlatform.App.ViewModels.Test
{
    public class TestViewModel
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;

        public List<QuestionViewModel> Questions { get; set; } = default!;
    }
}
