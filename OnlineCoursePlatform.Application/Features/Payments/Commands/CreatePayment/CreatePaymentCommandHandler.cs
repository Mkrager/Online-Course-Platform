using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Payment> _paymentRepository;

        public CreatePaymentCommandHandler(IMapper mapper, IAsyncRepository<Payment> paymentRepository)
        {
            _mapper = mapper;
            _paymentRepository = paymentRepository;
        }
        public async Task<Guid> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var @payment = _mapper.Map<Payment>(request);

            @payment = await _paymentRepository.AddAsync(payment);

            return @payment.Id;
        }
    }
}
