using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels.Course;
using OnlineCoursePlatform.App.ViewModels.Lesson;
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

        public Task<ApiResponse<Guid>> CreateLesson(LessonViewModel lessonViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LessonViewModel>> GetCourseLessons(Guid courseId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7275/api/lesson/{courseId}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var lessonList = JsonSerializer.Deserialize<List<LessonViewModel>>(responseContent, _jsonOptions);

                return lessonList;
            }

            return new List<LessonViewModel>();
        }
    }
}
