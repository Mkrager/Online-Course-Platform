namespace OnlineCoursePlatform.Domain.Entities
{
    public class Enrollment
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        //public string StudentId { get; set; } = string.Empty;
        public DateTime EnrolledAt { get; set; }
        public int Progress { get; set; }

        public Course Course { get; set; } = default!;
    }
}
