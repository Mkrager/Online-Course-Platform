using OnlineCoursePlatform.Domain.Common;

namespace OnlineCoursePlatform.Domain.Entities
{
    public class TeacherApplication : RequestEntity
    {
        public string Bio { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;

        public ICollection<string>? CertificatesUrls { get; set; }
    }
}
