using System.ComponentModel.DataAnnotations;

namespace OnlineCoursePlatform.App.ViewModels.Lesson
{
    public class LessonViewModel
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }

        [Required(ErrorMessage = "Title required")]
        [MaxLength(100, ErrorMessage = "Title must not exceed 100 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description required")]
        public string Description { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Order required")]
        [Range(0, int.MaxValue, ErrorMessage = "Order should be a positive value")]
        public int Order { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
