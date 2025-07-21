using OnlineCoursePlatform.Domain.Common;

namespace OnlineCoursePlatform.Domain.Entities
{
    public class Certificate : BaseEntity
    {
        public Guid EnrollmentId { get; set; }
        public string CertificateUrl { get; set; } = string.Empty;
        public DateTime IssuedAt { get; set; }

        public Enrollment Enrollment { get; set; } = default!;
    }
}
