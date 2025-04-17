namespace OnlineCoursePlatform.Domain.Entities
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string InstructorId { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Price { get; set; }
        public bool IsPublished { get; set; }
        public string ThumbnailUrl { get; set; } = string.Empty;

        public Category Category { get; set; } = default!;
        public ICollection<Lesson>? Lessons { get; set; }
    }
}
