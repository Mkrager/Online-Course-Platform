namespace OnlineCoursePlatform.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string PayPalOrderId { get; set; } = string.Empty;
        public string PayerId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Status { get; set; } = "Created";
        public DateTime CreatedAt { get; set; }

        public Course Course { get; set; } = default!;
    }
}
