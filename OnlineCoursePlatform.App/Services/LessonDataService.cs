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
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(lessonViewModel),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync("lesson", content);

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
                var content = new StringContent(
                    JsonSerializer.Serialize(lessonViewModel),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PutAsync("lesson", content);

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
                var response = await _httpClient.DeleteAsync($"lesson/{id}");

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
            var response = await _httpClient.GetAsync($"lesson/by-course/{courseId}");

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
            var response = await _httpClient.GetAsync($"Lesson/{id}");

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
