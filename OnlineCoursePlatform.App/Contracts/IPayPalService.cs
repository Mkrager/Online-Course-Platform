using OnlineCoursePlatform.App.Services;
using OnlineCoursePlatform.App.ViewModels.PayPal;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface IPayPalService
    {
        Task<ApiResponse<string>> CreateOrderAsync(Guid courseId);
        Task<ApiResponse<bool>> CaptureOrderAsync(Guid paymentId, string token, string payerId);
        Task<ApiResponse> CancelOrderAsync(CancelOrderViewModel cancelOrderViewModel);
    }
}
