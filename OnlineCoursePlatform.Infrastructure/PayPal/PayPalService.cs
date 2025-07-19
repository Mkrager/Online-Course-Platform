using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using OnlineCoursePlatform.Application.Contracts.Infrastructure;
using OnlineCoursePlatform.Infrastructure.Configuration;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using OnlineCoursePlatform.Application.DTOs.PayPal;

namespace OnlineCoursePlatform.Infrastructure.PayPal
{
    public class PayPalService : IPayPalService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly PayPalSettings _settings;
        private readonly IMemoryCache _cache;

        public PayPalService(IHttpClientFactory factory, IOptions<PayPalSettings> settings, IMemoryCache cache)
        {
            _httpClientFactory = factory;
            _settings = settings.Value;
            _cache = cache;
        }

        public async Task<CreateOrderResponse> CreateOrderAsync(decimal amount, string returnUrl, string cancelUrl)
        {
            var client = _httpClientFactory.CreateClient();
            var token = await GetAccessTokenAsync();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var orderPayload = new
            {
                intent = "CAPTURE",
                purchase_units = new[]
                {
                new
                {
                    amount = new
                    {
                        currency_code = "USD",
                        value = amount.ToString("F2", CultureInfo.InvariantCulture)
                    }
                }
            },
                application_context = new
                {
                    return_url = returnUrl,
                    cancel_url = cancelUrl
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(orderPayload), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_settings.BaseUrl}/v2/checkout/orders", content);
            var responseData = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

            string orderId = responseData.RootElement.GetProperty("id").GetString();

            var approveLink = responseData.RootElement.GetProperty("links")
                .EnumerateArray()
                .First(x => x.GetProperty("rel").GetString() == "approve")
                .GetProperty("href").GetString();
            return new CreateOrderResponse()
            {
                OrderId = orderId,
                RedirectUrl = approveLink
            };

        }

        public async Task<bool> CaptureOrderAsync(string orderId)
        {
            var client = _httpClientFactory.CreateClient();
            var token = await GetAccessTokenAsync();

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            using var content = new StringContent("{}", Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{_settings.BaseUrl}/v2/checkout/orders/{orderId}/capture")
            {
                Content = content
            };
            request.Headers.Add("Prefer", "return=representation");

            using var response = await client.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException($"PayPal capture failed: {json}");

            var doc = JsonDocument.Parse(json);
            var status = doc.RootElement.GetProperty("status").GetString();

            return status == "COMPLETED";
        }
        private async Task<string> GetAccessTokenAsync()
        {
            if (_cache.TryGetValue("PayPalToken", out string token)) return token;

            var client = _httpClientFactory.CreateClient();
            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_settings.ClientId}:{_settings.Secret}"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            var content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await client.PostAsync($"{_settings.BaseUrl}/v1/oauth2/token", content);
            var result = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

            token = result.RootElement.GetProperty("access_token").GetString();
            var expiresIn = result.RootElement.GetProperty("expires_in").GetInt32();

            _cache.Set("PayPalToken", token, TimeSpan.FromSeconds(expiresIn - 60));
            return token;
        }
    }
}
