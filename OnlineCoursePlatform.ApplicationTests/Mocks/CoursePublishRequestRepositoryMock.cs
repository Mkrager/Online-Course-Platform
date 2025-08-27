using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.UnitTests.Mocks
{
    public class CoursePublishRequestRepositoryMock
    {
        public static Mock<ICoursePublishRequestRepository> GetCoursePublishRequest()
        {
            var coursePublishRequests = new List<CoursePublishRequest>()
            {
                new CoursePublishRequest()
                {
                    Id = Guid.Parse("6ba684fb-e3bc-418c-8971-2f302b43daf6"),
                    Status = CoursePublishStatus.Pending
                },
                new CoursePublishRequest()
                {
                    Id = Guid.Parse("e706a9cf-6e56-46ed-896a-eadcad69c90f"),
                }
            };

            var mockRepository = new Mock<ICoursePublishRequestRepository>();

            mockRepository.Setup(repo => repo.ListAllAsync())
                .ReturnsAsync(coursePublishRequests);

            mockRepository.Setup(r => r.AddAsync(It.IsAny<CoursePublishRequest>()))
                .ReturnsAsync((CoursePublishRequest coursePublishRequest) =>
                {
                    coursePublishRequests.Add(coursePublishRequest);
                    return coursePublishRequest;
                });

            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => coursePublishRequests.FirstOrDefault(x => x.Id == id));


            return mockRepository;
        }
    }
}
