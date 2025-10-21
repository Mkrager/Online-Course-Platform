using OnlineCoursePlatform.Domain.Common;

namespace OnlineCoursePlatform.Domain.Entities
{
    public class SupportTicketMessage : AuditableEntity
    {
        public Guid TicketId { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsFromStaff { get; set; }

        public SupportTicket Ticket { get; set; } = default!;
    }
}