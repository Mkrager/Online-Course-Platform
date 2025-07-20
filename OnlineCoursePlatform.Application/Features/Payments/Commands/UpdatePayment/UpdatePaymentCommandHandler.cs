using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Payments.Commands.UpdatePayment
{
    public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Payment> _paymentRepository;

        public UpdatePaymentCommandHandler(IMapper mapper, IAsyncRepository<Payment> paymentRepository)
        {
            _mapper = mapper;
            _paymentRepository = paymentRepository;
        }

        public async Task<Unit> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var paymentToUpdate = await _paymentRepository.GetByIdAsync(request.Id);

            if (paymentToUpdate == null)
                throw new NotFoundException(nameof(Payment), request.Id);

            _mapper.Map(request, paymentToUpdate, typeof(UpdatePaymentCommand), typeof(Payment));

            await _paymentRepository.UpdateAsync(paymentToUpdate);

            return Unit.Value;
        }
    }
}
