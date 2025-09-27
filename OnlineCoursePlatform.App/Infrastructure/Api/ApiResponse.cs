using System.Net;

namespace OnlineCoursePlatform.App.Infrastructure.Api
{
    public class ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T? Data { get; set; }
        public string? ErrorText { get; set; }
        public bool IsSuccess => ((int)StatusCode >= 200 && (int)StatusCode <= 299);

        public ApiResponse(HttpStatusCode statusCode, T? data = default, string? errorText = null)
        {
            StatusCode = statusCode;
            Data = data;
            ErrorText = errorText;
        }
    }

    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? ErrorText { get; set; }
        public bool IsSuccess => ((int)StatusCode >= 200 && (int)StatusCode <= 299);

        public ApiResponse(HttpStatusCode statusCode, string? errorText = null)
        {
            StatusCode = statusCode;
            ErrorText = errorText;
        }
    }
}
