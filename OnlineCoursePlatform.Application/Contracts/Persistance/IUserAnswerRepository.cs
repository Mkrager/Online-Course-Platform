using OnlineCoursePlatform.Application.Features.TestAttemps.Commands.EndAttempt;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Contracts.Persistance
{
    public interface IUserAnswerRepository : IAsyncRepository<UserAnswer>
    {
        Task<List<UserAnswer>> PopulateIsCorrectAsync(List<UserAnswer> userAnswerDtos);
    }
}
