using OnlineCoursePlatform.Application.DTOs.PayPal;

namespace OnlineCoursePlatform.Application.Contracts.Infrastructure
{
    public interface IPayPalService
    {
        Task<CreateOrderResponse> CreateOrderAsync(decimal amount, string returnUrl, string cancelUrl);
        Task<bool> CaptureOrderAsync(string orderId);
    }
}
