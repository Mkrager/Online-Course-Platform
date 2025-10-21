using OnlineCoursePlatform.Domain.Common;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Domain.Entities
{
    //TODO: Finish later
    public class SupportTicket : AuditableEntity
    {
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public SupportStatus SupportStatus { get; set; }

        public ICollection<SupportTicketMessage>? Messages { get; set; }
    }
}
