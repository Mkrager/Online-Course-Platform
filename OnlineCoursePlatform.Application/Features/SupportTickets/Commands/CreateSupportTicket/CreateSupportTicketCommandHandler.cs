using AutoMapper;
using OnlineCoursePlatform.Application.Common.Handlers.Commands;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.SupportTickets.Commands.CreateSupportTicket
{
    public class CreateSupportTicketCommandHandler : CreateEntityCommandHandler<CreateSupportTicketCommand, SupportTicket, Guid>
    {
        public CreateSupportTicketCommandHandler(IAsyncRepository<SupportTicket> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override Guid BuildResponse(SupportTicket entity) => entity.Id;
    }
}
