using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Payments.Queries.GetPaymentDetail
{
    public class GetPaymentDetailQueryHandler : IRequestHandler<GetPaymentDetailQuery, PaymentDetailVm>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Payment> _paymentRepository;

        public GetPaymentDetailQueryHandler(IMapper mapper, IAsyncRepository<Payment> paymentRepository)
        {
            _mapper = mapper;
            _paymentRepository = paymentRepository;
        }

        public async Task<PaymentDetailVm> Handle(GetPaymentDetailQuery request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(request.Id);

            if (payment == null)
                throw new NotFoundException(nameof(Payment), request.Id);

            return _mapper.Map<PaymentDetailVm>(payment);
        }
    }
}
