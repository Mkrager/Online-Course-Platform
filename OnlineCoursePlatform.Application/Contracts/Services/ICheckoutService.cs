using OnlineCoursePlatform.Application.DTOs.PayPal;

namespace OnlineCoursePlatform.Application.Contracts.Services
{
    public interface ICheckoutService
    {
        Task<string> CreateOrderAsync(Guid courseId, string userId);
        Task<bool> CaptureOrderAsync(CaptureOrderRequest captureOrderRequest);
    }
}
