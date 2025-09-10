using OnlineCoursePlatform.App.ViewModels.Category;
using OnlineCoursePlatform.App.ViewModels.Level;
using System.ComponentModel.DataAnnotations;

namespace OnlineCoursePlatform.App.ViewModels.Course
{
    public class CourseDetailViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(50, ErrorMessage = "Description must not less 50 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Level is required")]
        public Guid LevelId { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Price should be a positive value")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public string ThumbnailUrl { get; set; } = string.Empty;
        //public bool IsPublished { get; set; }
        public CategoryViewModel Category { get; set; } = default!;
        public LevelViewModel Level { get; set; } = default!;
    }
}
