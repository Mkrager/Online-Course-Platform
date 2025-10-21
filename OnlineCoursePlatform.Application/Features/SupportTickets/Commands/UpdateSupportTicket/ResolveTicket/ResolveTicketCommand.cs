using MediatR;

namespace OnlineCoursePlatform.Application.Features.SupportTickets.Commands.UpdateSupportTicket.ResolveTicket
{
    public class ResolveTicketCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
