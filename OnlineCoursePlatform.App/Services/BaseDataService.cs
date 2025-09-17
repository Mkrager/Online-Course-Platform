using OnlineCoursePlatform.App.Contracts;
using System.Net.Http.Headers;
using System.Text.Json;

namespace OnlineCoursePlatform.App.Services
{
    public abstract class BaseDataService
    {
        protected readonly HttpClient _httpClient;
        protected readonly IAuthenticationService _authenticationService;
        protected readonly JsonSerializerOptions _jsonOptions;

        protected BaseDataService(IHttpClientFactory httpClientFactory, IAuthenticationService authenticationService)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _authenticationService = authenticationService;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        protected void AddAuthHeader()
        {
            string accessToken = _authenticationService.GetAccessToken();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);
        }

        protected async Task<T?> DeserializeResponse<T>(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseContent, _jsonOptions);
        }
    }
}