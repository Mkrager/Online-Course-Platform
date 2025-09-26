using OnlineCoursePlatform.App.Helpers;
using OnlineCoursePlatform.App.Infrastructure.Api;
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

        protected async Task<ApiResponse<T>> HandleResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeResponse<T>(response);
                return new ApiResponse<T>(response.StatusCode, result!);
            }

            try
            {
                var errorMessages = await DeserializeResponse<List<string>>(response);
                return new ApiResponse<T>(response.StatusCode, default!, errorMessages?.FirstOrDefault() ?? "Unknown error");
            }
            catch (InvalidOperationException ex)
            {
                return new ApiResponse<T>(response.StatusCode, default!, ex.Message);
            }
            catch
            {
                return new ApiResponse<T>(response.StatusCode, default!, "Unexpected error occurred.");
            }
        }

        protected async Task<ApiResponse> HandleResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return new ApiResponse(response.StatusCode);
            }

            try
            {
                var errorMessages = await DeserializeResponse<List<string>>(response);
                return new ApiResponse(response.StatusCode, errorMessages?.FirstOrDefault() ?? "Unknown error");
            }
            catch (InvalidOperationException ex)
            {
                return new ApiResponse(response.StatusCode, ex.Message);
            }
            catch
            {
                return new ApiResponse(response.StatusCode, "Unexpected error occurred.");
            }
        }

        protected async Task<T?> DeserializeResponse<T>(HttpResponseMessage response, CancellationToken cancellationToken = default)
        {
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            try
            {
                return JsonSerializer.Deserialize<T>(content, _jsonOptions);
            }
            catch (JsonException)
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