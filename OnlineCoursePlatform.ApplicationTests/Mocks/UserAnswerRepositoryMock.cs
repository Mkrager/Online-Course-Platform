using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.UnitTests.Mocks
{
    public class UserAnswerRepositoryMock
    {
        public static Mock<IUserAnswerRepository> GetUserAnswerRepository()
        {
            var mockRepository = new Mock<IUserAnswerRepository>();

            mockRepository.Setup(repo => repo.PopulateIsCorrectAsync(It.IsAny<List<UserAnswer>>()))
                .ReturnsAsync((List<UserAnswer> userAnswers) =>
                {
                    foreach (var ua in userAnswers)
                    {
                        ua.IsCorrect = true;
                    }
                    return userAnswers;
                });

            return mockRepository;
        }
    }
}
