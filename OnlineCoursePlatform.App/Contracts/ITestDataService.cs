using OnlineCoursePlatform.App.Services;
using OnlineCoursePlatform.App.ViewModels.Test;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface ITestDataService
    {
        Task<TestViewModel> GetTestById(Guid id);
        Task<List<TestViewModel>> GetTestByLessonId(Guid lessonId);
        Task<ApiResponse<Guid>> CreateTest(TestViewModel testViewModel);
        Task<ApiResponse> DeleteTest(Guid id);
        Task<ApiResponse> UpdateTest(Guid id);
    }
}
