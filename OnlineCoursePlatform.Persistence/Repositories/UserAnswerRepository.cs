using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Persistence.Repositories
{
    public class UserAnswerRepository : BaseRepository<UserAnswer>, IUserAnswerRepository
    {
        public UserAnswerRepository(OnlineCoursePlatformDbContext dbContext) : base(dbContext)
        {
            
        }
        public async Task<List<UserAnswer>> PopulateIsCorrectAsync(List<UserAnswer> userAnswers)
        {
            var answerIds = userAnswers.Select(x => x.AnswerId).ToList();

            var allAnswers = await _dbContext.Answers
                .Where(x => answerIds.Contains(x.Id))
                .ToDictionaryAsync(a => a.Id, a => a.IsCorrect);

            foreach (var userAnswer in userAnswers)
            {
                if(allAnswers.TryGetValue(userAnswer.AnswerId, out var isCorrect))
                    userAnswer.IsCorrect = isCorrect;
                else
                    userAnswer.IsCorrect = false;
            }

            return userAnswers;
        }
    }
}
