using MediatR;

namespace OnlineCoursePlatform.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommand : IRequest<Guid>
    {
        public string UserId { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public string PayerId { get; set; } = string.Empty;
    }
}
