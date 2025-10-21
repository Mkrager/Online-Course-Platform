using AutoMapper;
using OnlineCoursePlatform.Application.Common.Handlers;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Features.SupportTickets.Commands.UpdateSupportTicket.ResolveTicket
{
    public class ResolveTicketCommandHandler : UpdateEntityCommandHandler<ResolveTicketCommand, SupportTicket>
    {
        public ResolveTicketCommandHandler(IAsyncRepository<SupportTicket> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override Guid GetEntityId(ResolveTicketCommand command) => command.Id;

        protected override Task BeforeUpdateAsync(SupportTicket entity)
        {
            entity.Status = SupportStatus.Resolved;
            return Task.CompletedTask;
        }
    }
}