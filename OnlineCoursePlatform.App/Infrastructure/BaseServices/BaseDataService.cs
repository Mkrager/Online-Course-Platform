using OnlineCoursePlatform.App.Helpers;
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
        protected async Task<T?> DeserializeResponse<T>(HttpResponseMessage response, CancellationToken cancellationToken = default)
        {
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            try
            {
                return JsonSerializer.Deserialize<T>(content, _jsonOptions);
            }
            catch (JsonException ex)
            {
                var errorMessage = JsonErrorHelper.GetErrorMessage(content);

                return HandleError<T>(errorMessage);
            }
        }

        private static T? HandleError<T>(string errorMessage)
        {
            if (typeof(T) == typeof(string))
                return (T?)(object)errorMessage;

            throw new InvalidOperationException(errorMessage);
        }
    }
}