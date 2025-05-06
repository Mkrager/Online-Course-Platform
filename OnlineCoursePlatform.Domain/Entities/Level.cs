namespace OnlineCoursePlatform.Domain.Entities
{
    public class Level
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }

        public ICollection<Course>? Courses { get; set; }
    }
}
