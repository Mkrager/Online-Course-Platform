using OnlineCoursePlatform.App.Services;
using OnlineCoursePlatform.App.ViewModels.Test;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface ITestDataService
    {
        Task<ApiResponse<Guid>> CreateTest(TestViewModel testViewModel);
        Task<ApiResponse> DeleteTest(Guid id);
    }
}
