using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.Infrastructure.BaseServices;
using OnlineCoursePlatform.App.ViewModels.PayPal;
using System.Text;
using System.Text.Json;

namespace OnlineCoursePlatform.App.Services
{
    public class PayPalService : BaseDataService, IPayPalService
    {
        public PayPalService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<ApiResponse<string>> CreateOrderAsync(Guid courseId)
        {

            var response = await _httpClient.PostAsync($"paypal/create-order?courseId={courseId}", null);
            return await HandleResponse<string>(response);
        }

        public async Task<ApiResponse<bool>> CaptureOrderAsync(CaptureOrderRequest captureOrderRequest)
        {

            var content = new StringContent(
                JsonSerializer.Serialize(captureOrderRequest),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("paypal/capture-order", content);
            return await HandleResponse<bool>(response);
        }

        public async Task<ApiResponse> CancelOrderAsync(CancelOrderViewModel cancelOrderViewModel)
        {

            var content = new StringContent(
                JsonSerializer.Serialize(cancelOrderViewModel),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PatchAsync("paypal/cancel", content);
            return await HandleResponse(response);
        }
    }
}