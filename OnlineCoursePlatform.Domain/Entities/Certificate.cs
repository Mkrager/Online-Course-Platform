namespace OnlineCoursePlatform.Domain.Entities
{
    public class Certificate
    {
        public Guid Id { get; set; }
        public Guid EnrollmentId { get; set; }
        public string CertificateUrl { get; set; } = string.Empty;
        public DateTime IssuedAt { get; set; }

        public Enrollment Enrollment { get; set; } = default!;
    }
}
