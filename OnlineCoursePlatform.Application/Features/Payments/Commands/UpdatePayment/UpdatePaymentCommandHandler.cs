using AutoMapper;
using OnlineCoursePlatform.Application.Common.Handlers;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Payments.Commands.UpdatePayment
{
    public class UpdatePaymentCommandHandler : UpdateEntityCommandHandler<UpdatePaymentCommand, Payment>
    {
        public UpdatePaymentCommandHandler(IAsyncRepository<Payment> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override Guid GetEntityId(UpdatePaymentCommand command) => command.Id;
    }
}