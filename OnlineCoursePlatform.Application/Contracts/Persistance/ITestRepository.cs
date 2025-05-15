using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Contracts.Persistance
{
    public interface ITestRepository : IAsyncRepository<Test>
    {
        Task<Test> GetTestWithQuestionsAndAnswers(Guid id);
        Task<List<Test>> GetTestsByUserId(string userId);
    }
}
