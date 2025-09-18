using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.ViewModels.Lesson;
using System.Text;
using System.Text.Json;

namespace OnlineCoursePlatform.App.Services
{
    public class LessonDataService : ILessonDataService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public LessonDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<ApiResponse<Guid>> CreateLesson(LessonViewModel lessonViewModel)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7275/api/lesson")
                {
                    Content = new StringContent(JsonSerializer.Serialize(lessonViewModel), Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var lessonId = JsonSerializer.Deserialize<Guid>(responseContent);

                    return new ApiResponse<Guid>(System.Net.HttpStatusCode.OK, lessonId);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessages = JsonSerializer.Deserialize<List<string>>(errorContent);
                return new ApiResponse<Guid>(System.Net.HttpStatusCode.BadRequest, Guid.Empty, errorMessages.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse<Guid>(System.Net.HttpStatusCode.BadRequest, Guid.Empty, ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateLesson(LessonViewModel lessonViewModel)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7275/api/lesson")
                {
                    Content = new StringContent(JsonSerializer.Serialize(lessonViewModel), Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
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

        public async Task<ApiResponse> DeleteLesson(Guid id)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7275/api/lesson/{id}");

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
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

        public async Task<List<LessonViewModel>> GetCourseLessons(Guid courseId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7275/api/lesson/by-course/{courseId}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var lessonList = JsonSerializer.Deserialize<List<LessonViewModel>>(responseContent, _jsonOptions);

                return lessonList;
            }

            return new List<LessonViewModel>();
        }

        public async Task<LessonViewModel> GetLessonById(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7275/api/Lesson/{id}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var lesson = JsonSerializer.Deserialize<LessonViewModel>(responseContent, _jsonOptions);

                return lesson;
            }

            return new LessonViewModel();
        }
    }
}
