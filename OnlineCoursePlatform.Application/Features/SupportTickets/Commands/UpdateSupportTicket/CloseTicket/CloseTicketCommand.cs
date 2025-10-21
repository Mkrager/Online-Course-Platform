using MediatR;

namespace OnlineCoursePlatform.Application.Features.SupportTickets.Commands.UpdateSupportTicket.CloseTicket
{
    public class CloseTicketCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
