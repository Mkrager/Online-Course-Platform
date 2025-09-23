using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Persistence.Repositories;
using OnlineCoursePlatform.Persistence;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Persistence.Interceptors;

namespace OnlineCoursePlatfrom.Persistence.InregrationTests
{
    public class UserAnswerRepositoryTests
    {
        private readonly OnlineCoursePlatformDbContext _dbContext;
        private readonly UserAnswerRepository _repository;
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly string _currentUserId;

        public UserAnswerRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<OnlineCoursePlatformDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _currentUserId = "00000000-0000-0000-0000-000000000000";
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            _currentUserServiceMock.Setup(m => m.UserId).Returns(_currentUserId);

            _auditableEntitySaveChangesInterceptor = new AuditableEntitySaveChangesInterceptor(_currentUserServiceMock.Object);
            _dbContext = new OnlineCoursePlatformDbContext(options, _auditableEntitySaveChangesInterceptor);
            _repository = new UserAnswerRepository(_dbContext);
        }

        [Fact]
        public async Task PopulateIsCorrectAsync_ReturnsCorrectUserAnswerValue()
        {
            var testId = Guid.NewGuid();
            var question1 = new Question { Id = Guid.NewGuid(), TestId = testId, Text = "What is 2 + 2?" };
            var question2 = new Question { Id = Guid.NewGuid(), TestId = testId, Text = "What is the capital of France?" };

            var correctAnswer1 = new Answer { Id = Guid.NewGuid(), QuestionId = question1.Id, Text = "4", IsCorrect = true };
            var wrongAnswer1 = new Answer { Id = Guid.NewGuid(), QuestionId = question1.Id, Text = "5", IsCorrect = false };

            var correctAnswer2 = new Answer { Id = Guid.NewGuid(), QuestionId = question2.Id, Text = "Paris", IsCorrect = true };
            var wrongAnswer2 = new Answer { Id = Guid.NewGuid(), QuestionId = question2.Id, Text = "London", IsCorrect = false };

            _dbContext.Questions.AddRange(question1, question2);
            _dbContext.Answers.AddRange(correctAnswer1, wrongAnswer1, correctAnswer2, wrongAnswer2);
            await _dbContext.SaveChangesAsync();

            var userAnswers = new List<UserAnswer>
            {
                new UserAnswer { AnswerId = correctAnswer1.Id, QuestionId = question1.Id },
                new UserAnswer { AnswerId = wrongAnswer2.Id, QuestionId = question2.Id }
            };

            var result = await _repository.PopulateIsCorrectAsync(userAnswers);

            Assert.Equal(2, result.Count);
            Assert.True(result[0].IsCorrect);
            Assert.False(result[1].IsCorrect);
        }
    }
}
