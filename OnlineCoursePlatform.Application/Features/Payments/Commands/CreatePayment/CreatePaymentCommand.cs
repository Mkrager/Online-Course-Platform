using MediatR;

namespace OnlineCoursePlatform.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommand : IRequest<Guid>
    {
        public string PayPalOrderId { get; set; } = string.Empty;
        public Guid CourseId { get; set; }
    }
}
