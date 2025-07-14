namespace OnlineCoursePlatform.Application.Contracts.Infrastructure
{
    public interface IPayPalService
    {
        Task<string> GetAccessTokenAsync();
        Task<string> CreateOrderAsync(decimal amount, string returnUrl, string cancelUrl);
        Task<bool> CaptureOrderAsync(string orderId);
    }
}
