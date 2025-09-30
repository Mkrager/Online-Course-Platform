using AutoMapper;
using OnlineCoursePlatform.Application.Common.Handlers.Queries;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Payments.Queries.GetPaymentDetail
{
    public class GetPaymentDetailQueryHandler : GetEntityByIdQueryHandler<GetPaymentDetailQuery, Payment, PaymentDetailVm>
    {
        public GetPaymentDetailQueryHandler(IAsyncRepository<Payment> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override Guid GetEntityId(GetPaymentDetailQuery query) => query.Id;
    }
}