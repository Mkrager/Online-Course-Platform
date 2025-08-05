using OnlineCoursePlatform.App.Services;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface IPayPalService
    {
        Task<ApiResponse<string>> CreateOrderAsync(Guid courseId);
        Task<ApiResponse<bool>> CaptureOrderAsync(Guid paymentId, string token, string payerId);
    }
}
