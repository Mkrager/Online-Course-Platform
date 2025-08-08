using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Contracts.Persistance
{
    public interface ITestRepository : IAsyncRepository<Test>
    {
        Task<Test> GetTestWithQuestionsAndAnswersAsync(Guid id);
        Task<List<Test>> GetTestsByLessonIdAsync(Guid lessonId);
    }
}
