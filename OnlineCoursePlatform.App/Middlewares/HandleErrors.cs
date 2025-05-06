using OnlineCoursePlatform.App.Services;

namespace OnlineCoursePlatform.App.Middlewares
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
    }
}
