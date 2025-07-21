using MediatR;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Features.Payments.Commands.UpdatePayment
{
    public class UpdatePaymentCommand : IRequest
    {
        public Guid Id { get; set; }
        public string PayerId { get; set; } = string.Empty;
        public string PayPalOrderId { get; set; } = string.Empty;
        public OrderStatus Status { get; set; }
    }
}
