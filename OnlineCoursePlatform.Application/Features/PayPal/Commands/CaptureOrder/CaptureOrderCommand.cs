using MediatR;

namespace OnlineCoursePlatform.Application.Features.PayPal.Commands.CaptureOrder
{
    public class CaptureOrderCommand : IRequest<bool>
    {
        public string OrderId { get; set; } = string.Empty;
        public string PayerId { get; set; } = string.Empty;
    }
}
