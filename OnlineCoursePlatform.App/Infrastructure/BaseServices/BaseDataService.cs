using System.Text.Json;

namespace OnlineCoursePlatform.App.Infrastructure.BaseServices
{
    public abstract class BaseDataService
    {
        protected readonly HttpClient _httpClient;
        protected readonly JsonSerializerOptions _jsonOptions;

        protected BaseDataService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }
        protected async Task<T?> DeserializeResponse<T>(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseContent, _jsonOptions);
        }
    }
}