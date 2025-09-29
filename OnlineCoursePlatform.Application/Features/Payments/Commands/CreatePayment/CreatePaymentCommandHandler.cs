using AutoMapper;
using OnlineCoursePlatform.Application.Common.Handlers.Commands;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandHandler : CreateEntityCommandHandler<CreatePaymentCommand, Payment, Guid>
    {
        public CreatePaymentCommandHandler(IAsyncRepository<Payment> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override Guid BuildResponse(Payment entity) => entity.Id;
    }
}
