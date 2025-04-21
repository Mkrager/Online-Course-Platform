using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Contracts.Persistance
{
    public interface ITestRepository
    {
        Task<Test> GetTestWithQuestionsAndAnswers(Guid id);
    }
}
