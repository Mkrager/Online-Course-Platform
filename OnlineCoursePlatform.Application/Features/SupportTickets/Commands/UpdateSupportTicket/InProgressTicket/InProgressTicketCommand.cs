using MediatR;

namespace OnlineCoursePlatform.Application.Features.SupportTickets.Commands.UpdateSupportTicket.InProgressTicket
{
    public class InProgressTicketCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
