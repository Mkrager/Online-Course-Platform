using MediatR;

namespace OnlineCoursePlatform.Application.Features.SupportTickets.Commands.CreateSupportTicket
{
    public class CreateSupportTicketCommand : IRequest<Guid>
    {
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}