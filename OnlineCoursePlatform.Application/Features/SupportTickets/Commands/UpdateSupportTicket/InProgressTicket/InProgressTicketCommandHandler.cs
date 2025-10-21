using AutoMapper;
using OnlineCoursePlatform.Application.Common.Handlers;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Features.SupportTickets.Commands.UpdateSupportTicket.InProgressTicket
{
    public class InProgressTicketCommandHandler : UpdateEntityCommandHandler<InProgressTicketCommand, SupportTicket>
    {
        public InProgressTicketCommandHandler(IAsyncRepository<SupportTicket> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override Guid GetEntityId(InProgressTicketCommand command) => command.Id;

        protected override Task BeforeUpdateAsync(SupportTicket entity)
        {
            entity.Status = SupportStatus.InProgress;
            return Task.CompletedTask;
        }
    }
}