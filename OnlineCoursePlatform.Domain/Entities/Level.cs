namespace OnlineCoursePlatform.Domain.Entities
{
    public class Level
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Course>? Courses { get; set; }
    }
}
