using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Persistence.Repositories;
using OnlineCoursePlatform.Persistence;
using OnlineCoursePlatform.Domain.Entities;
using Moq;
using OnlineCoursePlatform.Application.Contracts;

namespace OnlineCoursePlatfrom.Persistence.InregrationTests
{
    public class TestRepositoryTests
    {
        private readonly OnlineCoursePlatformDbContext _dbContext;
        private readonly TestRepository _repository;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly string _currentUserId;

        public TestRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<OnlineCoursePlatformDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _currentUserId = "00000000-0000-0000-0000-000000000000";
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            _currentUserServiceMock.Setup(m => m.UserId).Returns(_currentUserId);

            _dbContext = new OnlineCoursePlatformDbContext(options, _currentUserServiceMock.Object);

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

        [Fact]
        public async Task GetTestsByUserId_WhenUserHasTests_ReturnsUserTests()
        {
            var lessonId = Guid.NewGuid();

            var lesson = new Lesson
            {
                Id = lessonId,
                Title = "Test"
            };

            _dbContext.Lessons.Add(lesson);
            await _dbContext.SaveChangesAsync();

            var testId = Guid.NewGuid();

            var test = new Test
            {
                Id = testId,
                Title = "Test",
                LessonId = lessonId
            };

            _dbContext.Tests.Add(test);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetTestsByUserId(_currentUserId);

            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
        }
    }
}
