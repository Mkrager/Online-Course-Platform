using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.ViewModels.Test;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface ITestDataService
    {
        Task<ApiResponse<TestViewModel>> GetTestById(Guid id);
        Task<ApiResponse<List<TestViewModel>>> GetTestByLessonId(Guid lessonId);
        Task<ApiResponse<Guid>> CreateTest(TestViewModel testViewModel);
        Task<ApiResponse> DeleteTest(Guid id);
        Task<ApiResponse> UpdateTest(TestViewModel testViewModel);
    }
}
