using OnlineCoursePlatform.App.Infrastructure.Api;

namespace OnlineCoursePlatform.App.Helpers
{
    public static class HandleErrors
    {
        public static string HandleResponse<T>(ApiResponse<T> response, string successMessage = "")
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return successMessage;
            }
            else
            {
                return response.ErrorText;
            }
        }

        public static string HandleResponse(ApiResponse response, string successMessage = "")
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return successMessage;
            }
            else
            {
                return response.ErrorText;
            }
        }

    }
}
