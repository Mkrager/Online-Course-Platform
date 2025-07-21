using MediatR;

namespace OnlineCoursePlatform.Application.Features.PayPal.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<CreateOrderResponse>
    {
        public Guid CourseId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string ReturnUrl { get; set; } = string.Empty;
        public string CancelUrl { get; set; } = string.Empty;
    }
}
