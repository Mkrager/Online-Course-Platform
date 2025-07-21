namespace OnlineCoursePlatform.Application.Features.PayPal.Commands.CreateOrder
{
    public class CreateOrderResponse
    {
        public string OrderId { get; set; } = string.Empty;
        public string RedirectUrl { get; set; } = string.Empty;
    }
}
