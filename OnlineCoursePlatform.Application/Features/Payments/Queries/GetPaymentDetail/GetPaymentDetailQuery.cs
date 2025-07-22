using MediatR;

namespace OnlineCoursePlatform.Application.Features.Payments.Queries.GetPaymentDetail
{
    public class GetPaymentDetailQuery : IRequest<PaymentDetailVm>
    {
        public Guid Id { get; set; }
    }
}
