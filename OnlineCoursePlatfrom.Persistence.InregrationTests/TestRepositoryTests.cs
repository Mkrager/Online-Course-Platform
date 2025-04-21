using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Persistence.Repositories;
using OnlineCoursePlatform.Persistence;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatfrom.Persistence.InregrationTests
{
    public class TestRepositoryTests
    {
        private readonly OnlineCoursePlatformDbContext _dbContext;
        private readonly TestRepository _repository;

        public TestRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<OnlineCoursePlatformDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _dbContext = new OnlineCoursePlatformDbContext(options);
            _repository = new TestRepository(_dbContext);
        }

        [Fact]
        public async Task GetTestWithQuestionAndAnswer_ReturnsTestWithQuestionAndAnswer()
        {
            var testId = Guid.NewGuid();
            var test = new Test
            {
                Id = testId,
                Title = "Test"
            };

            var questionTest1 = new Question { TestId = testId, Text = "Test question" };
            var questionTest2 = new Question { TestId = testId, Text = "Test question" };

            test.Questions = new List<Question> { questionTest1, questionTest2 };

            _dbContext.Tests.Add(test);
            _dbContext.Questions.AddRange(questionTest1, questionTest2);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetTestWithQuestionsAndAnswers(testId);

            Assert.NotNull(result);
            Assert.Equal(result.Title, "Test");
            Assert.Equal(2, result.Questions.Count);
        }

    }
}
