using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.UnitTests.Mocks
{
    public class TestAttemptRepositoryMock
    {
        public static Mock<IAsyncRepository<TestAttempt>> GetTestAttemptRepository()
        {
            var testAttempts = new List<TestAttempt>
            {
                new TestAttempt
                {
                    Id = Guid.Parse("9f2b5c3e-6d3e-4c9f-9b57-3a8c4f0b1207"),
                    IsCompleted = true,
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddMinutes(50),
                    UserId = "testUserId",
                    TestId = Guid.Parse("1d4a7b91-2e1f-4df6-9268-91f3f9f253b4")
                },
                new TestAttempt
                {
                    Id = Guid.Parse("b7f2e0c8-6e03-4bfc-9d9a-94d8b49aa0f2"),
                    IsCompleted = false,
                    StartTime = DateTime.UtcNow.AddDays(-40),
                    EndTime = DateTime.UtcNow.AddDays(-40).AddMinutes(20),
                    UserId = "testUserId2",
                    TestId = Guid.Parse("fa9db1a4-c3cd-4e5b-805f-d4a624ef34c1")
                }
            };

            var mockRepository = new Mock<IAsyncRepository<TestAttempt>>();

            mockRepository.Setup(r => r.ListAllAsync())
                .ReturnsAsync(testAttempts);

            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => testAttempts.FirstOrDefault(x => x.Id == id));

            mockRepository.Setup(r => r.AddAsync(It.IsAny<TestAttempt>()))
                .ReturnsAsync((TestAttempt testAttempt) =>
                {
                    testAttempts.Add(testAttempt);
                    return testAttempt;
                });

            mockRepository.Setup(r => r.UpdateAsync(It.IsAny<TestAttempt>()))
                .Callback((TestAttempt testAttempt) =>
                {
                    var oldTestAttempt = testAttempts.FirstOrDefault(x => x.Id == testAttempt.Id);
                    if (oldTestAttempt != null)
                    {
                        oldTestAttempt.EndTime = testAttempt.EndTime;
                        oldTestAttempt.IsCompleted = testAttempt.IsCompleted;
                    }
                });

            return mockRepository;
        }
    }
}
