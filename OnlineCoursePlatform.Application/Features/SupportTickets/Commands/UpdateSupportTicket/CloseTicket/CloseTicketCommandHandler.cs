using AutoMapper;
using OnlineCoursePlatform.Application.Common.Handlers;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Features.SupportTickets.Commands.UpdateSupportTicket.CloseTicket
{
    public class CloseTicketCommandHandler : UpdateEntityCommandHandler<CloseTicketCommand, SupportTicket>
    {
        public CloseTicketCommandHandler(IAsyncRepository<SupportTicket> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override Guid GetEntityId(CloseTicketCommand command) => command.Id;

        protected override Task BeforeUpdateAsync(SupportTicket entity)
        {
            entity.SupportStatus = SupportStatus.Closed;
            return Task.CompletedTask;
        }
    }
}
