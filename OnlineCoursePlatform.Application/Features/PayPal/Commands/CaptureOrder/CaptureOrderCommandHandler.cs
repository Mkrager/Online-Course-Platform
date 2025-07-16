using MediatR;
using OnlineCoursePlatform.Application.Contracts.Infrastructure;

namespace OnlineCoursePlatform.Application.Features.PayPal.Commands.CaptureOrder
{
    public class CaptureOrderCommandHandler : IRequestHandler<CaptureOrderCommand, bool>
    {
        private readonly IPayPalService _payPalService;
        public CaptureOrderCommandHandler(IPayPalService payPalService)
        {
            _payPalService = payPalService;
        }
        public async Task<bool> Handle(CaptureOrderCommand request, CancellationToken cancellationToken)
        {
            return await _payPalService.CaptureOrderAsync(request.OrderId);
        }
    }
}
