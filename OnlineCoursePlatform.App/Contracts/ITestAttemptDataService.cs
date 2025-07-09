using OnlineCoursePlatform.App.Services;
using OnlineCoursePlatform.App.ViewModels.TestAttempt;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface ITestAttemptDataService
    {
        Task<ApiResponse<Guid>> StartTestAttempt(StartTestAttemptViewModel startAttemptViewModel);
        Task<ApiResponse<Guid>> EndTestAttempt(EndTestAttemptViewModel endAttemptViewModel);
    }
}
