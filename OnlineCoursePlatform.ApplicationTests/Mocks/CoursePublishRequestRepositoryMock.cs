using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.UnitTests.Mocks
{
    public class CoursePublishRequestRepositoryMock
    {
        public static Mock<IAsyncRepository<CoursePublishRequest>> GetCoursePublishRequest()
        {
            var coursePublishRequests = new List<CoursePublishRequest>()
            {
                new CoursePublishRequest()
                {
                    Id = Guid.Parse("6ba684fb-e3bc-418c-8971-2f302b43daf6"),
                },
                new CoursePublishRequest()
                {
                    Id = Guid.Parse("e706a9cf-6e56-46ed-896a-eadcad69c90f"),
                }
            };

            var mockRepository = new Mock<IAsyncRepository<CoursePublishRequest>>();

            mockRepository.Setup(repo => repo.ListAllAsync())
                .ReturnsAsync(coursePublishRequests);

            return mockRepository;
        }
    }
}
