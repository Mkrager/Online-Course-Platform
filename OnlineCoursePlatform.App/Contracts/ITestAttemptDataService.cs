using OnlineCoursePlatform.App.Infrastructure.Api;
using OnlineCoursePlatform.App.ViewModels.TestAttempt;

namespace OnlineCoursePlatform.App.Contracts
{
    public interface ITestAttemptDataService
    {
        Task<ApiResponse<Guid>> StartTestAttempt(StartTestAttemptViewModel startAttemptViewModel);
        Task<ApiResponse> EndTestAttempt(EndTestAttemptViewModel endAttemptViewModel);
    }
}
