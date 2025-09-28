using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.ViewModels.PayPal;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface IPayPalService
    {
        Task<ApiResponse<CreateOrderResponse>> CreateOrderAsync(Guid courseId);
        Task<ApiResponse<bool>> CaptureOrderAsync(CaptureOrderRequest captureOrderRequest);
        Task<ApiResponse> CancelOrderAsync(CancelOrderViewModel cancelOrderViewModel);
    }
}
