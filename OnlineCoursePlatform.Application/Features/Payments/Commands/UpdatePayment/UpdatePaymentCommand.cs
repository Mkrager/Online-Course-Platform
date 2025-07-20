using MediatR;

namespace OnlineCoursePlatform.Application.Features.Payments.Commands.UpdatePayment
{
    public class UpdatePaymentCommand : IRequest
    {
        public Guid Id { get; set; }
        public string PayerId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
