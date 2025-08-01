using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.UnitTests.Mocks
{
    public class TestRepositoryMock
    {
        public static Mock<ITestRepository> GetTestRepository()
        {
            var tests = new List<Test>
            {
                new Test { Id = Guid.Parse("3f2a3a3e-27c9-4b65-bfb4-2b1e3d4b54ee"), Title = "Test1", LessonId = Guid.Parse("3f29e1a5-67b4-47f2-a726-05e45bdb2b4c") },
                new Test { Id = Guid.Parse("c1e9a0b2-5f3d-4427-8a3f-6db42c948ce4"), Title = "Test2" }

            };

            var mockRepository = new Mock<ITestRepository>();

            mockRepository.Setup(r => r.ListAllAsync())
                .ReturnsAsync(tests);

            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => tests.FirstOrDefault(x => x.Id == id));

            mockRepository.Setup(r => r.AddAsync(It.IsAny<Test>()))
                .ReturnsAsync((Test test) =>
                {
                    tests.Add(test);
                    return test;
                });

            mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Test>()))
                .Callback((Test test) =>
                {
                    var oldTest = tests.FirstOrDefault(x => x.Id == test.Id);
                    if (oldTest != null)
                    {
                        oldTest.Title = test.Title;
                    }
                });

            mockRepository.Setup(r => r.DeleteAsync(It.IsAny<Test>()))
                .Callback((Test test) => tests.Remove(test));

            mockRepository.Setup(repo => repo.GetTestWithQuestionsAndAnswers(It.IsAny<Guid>())).ReturnsAsync((Guid id) => tests.FirstOrDefault(x => x.Id == id));

            mockRepository.Setup(repo => repo.GetTestsByLessonId(It.IsAny<Guid>())).ReturnsAsync((Guid lessonId) => tests.Where(t => t.LessonId == lessonId).ToList());

            return mockRepository;
        }
    }
}
