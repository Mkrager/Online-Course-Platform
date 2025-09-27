using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.Infrastructure.BaseServices;
using OnlineCoursePlatform.App.ViewModels.Lesson;
using System.Text;
using System.Text.Json;

namespace OnlineCoursePlatform.App.Services
{
    public class LessonDataService : BaseDataService, ILessonDataService
    {
        public LessonDataService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<ApiResponse<Guid>> CreateLesson(LessonViewModel lessonViewModel)
        {

            var content = new StringContent(
                JsonSerializer.Serialize(lessonViewModel),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("lesson", content);
            return await HandleResponse<Guid>(response);
        }

        public async Task<ApiResponse> UpdateLesson(LessonViewModel lessonViewModel)
        {

            var content = new StringContent(
                JsonSerializer.Serialize(lessonViewModel),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PutAsync("lesson", content);
            return await HandleResponse(response);
        }

        public async Task<ApiResponse> DeleteLesson(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"lesson/{id}");
            return await HandleResponse(response);
        }

        public async Task<ApiResponse<List<LessonViewModel>>> GetCourseLessons(Guid courseId)
        {
            var response = await _httpClient.GetAsync($"lesson/by-course/{courseId}");
            return await HandleResponse<List<LessonViewModel>>(response);
        }

        public async Task<ApiResponse<LessonViewModel>> GetLessonById(Guid id)
        {
            var response = await _httpClient.GetAsync($"Lesson/{id}");
            return await HandleResponse<LessonViewModel>(response);
        }
    }
}