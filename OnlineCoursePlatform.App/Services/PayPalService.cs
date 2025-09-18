using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.ViewModels.PayPal;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace OnlineCoursePlatform.App.Services
{
    public class PayPalService : IPayPalService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly IAuthenticationService _authenticationService;

        public PayPalService(HttpClient httpClient, IAuthenticationService authenticationService)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _authenticationService = authenticationService;
        }
        public async Task<ApiResponse<string>> CreateOrderAsync(Guid courseId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7275/api/paypal/create-order?courseId={courseId}");
                string accessToken = _authenticationService.GetAccessToken();

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.SendAsync(request);


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
                var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7275/api/paypal/capture-order")
                {
                    Content = new StringContent(JsonSerializer.Serialize(captureOrderRequest), Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(request);

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
                var request = new HttpRequestMessage(HttpMethod.Patch, $"https://localhost:7275/api/paypal/cancel")
                {
                    Content = new StringContent(JsonSerializer.Serialize(cancelOrderViewModel), Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(request);

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