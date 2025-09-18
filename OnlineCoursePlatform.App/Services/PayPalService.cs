using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.Infrastructure.BaseServices;
using OnlineCoursePlatform.App.ViewModels.Lesson;
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
            try
            {
                var response = await _httpClient.PostAsync($"paypal/create-order?courseId={courseId}", null);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var result = JsonSerializer.Deserialize<CreateOrderResponse>(responseContent, _jsonOptions);

                    return new ApiResponse<string>(System.Net.HttpStatusCode.OK, result.Url);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessages = JsonSerializer.Deserialize<List<string>>(errorContent);
                return new ApiResponse<string>(System.Net.HttpStatusCode.BadRequest, string.Empty, errorMessages.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>(System.Net.HttpStatusCode.BadRequest, string.Empty, ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> CaptureOrderAsync(CaptureOrderRequest captureOrderRequest)
        {
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(captureOrderRequest),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync("paypal/capture-order", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var result = JsonSerializer.Deserialize<bool>(responseContent, _jsonOptions);

                    return new ApiResponse<bool>(System.Net.HttpStatusCode.OK, result);
                }
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessages = JsonSerializer.Deserialize<List<string>>(errorContent);
                return new ApiResponse<bool>(System.Net.HttpStatusCode.BadRequest, false, errorMessages.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(System.Net.HttpStatusCode.BadRequest, false, ex.Message);
            }
        }

        public async Task<ApiResponse> CancelOrderAsync(CancelOrderViewModel cancelOrderViewModel)
        {
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(cancelOrderViewModel),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync("paypal/cancel", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    return new ApiResponse(System.Net.HttpStatusCode.OK);
                }
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessages = JsonSerializer.Deserialize<List<string>>(errorContent);
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, errorMessages.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}