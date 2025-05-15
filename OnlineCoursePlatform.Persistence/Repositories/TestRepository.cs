using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Persistence.Repositories
{
    public class TestRepository : BaseRepository<Test>, ITestRepository
    {
        public TestRepository(OnlineCoursePlatformDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Test> GetTestWithQuestionsAndAnswers(Guid id)
        {
            var testWithQuestionAndAnswer = await _dbContext.Tests.Include(x => x.Questions).ThenInclude(x => x.Answers).FirstOrDefaultAsync(t => t.Id == id);
            return testWithQuestionAndAnswer;
        }

        public async Task<List<Test>> GetTestsByLessonId(Guid lessonId)
        {
            var userTests = await _dbContext.Tests.Where(u => u.LessonId == lessonId).ToListAsync();
            return userTests;
        }
    }
}
