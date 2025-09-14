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
                    CourseId =  Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b21e8"),
                    Status = CoursePublishStatus.Pending,
                    CreatedBy = "userId"
                },
                new CoursePublishRequest()
                {
                    Id = Guid.Parse("e706a9cf-6e56-46ed-896a-eadcad69c90f"),
                    CreatedBy = "userId"
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

            mockRepository.Setup(r => r.GetCoursePublishRequestByUserIdAsync(It.IsAny<string>()))
                .ReturnsAsync((string userId) => coursePublishRequests.Where(r => r.CreatedBy == userId).ToList());

            mockRepository.Setup(r => r.UpdateStatusAsync(It.IsAny<CoursePublishRequest>(), It.IsAny<CoursePublishStatus>(), It.IsAny<string?>()))
                .Callback((CoursePublishRequest coursePublishRequest, CoursePublishStatus newStatus, string? reason) =>
                {
                    var oldCoursePublishRequest = coursePublishRequests.FirstOrDefault(x => x.Id == coursePublishRequest.Id);
                    if (oldCoursePublishRequest != null)
                    {
                        oldCoursePublishRequest.Status = newStatus;
                        oldCoursePublishRequest.RejectReason = reason;
                    }
                });

            return mockRepository;
        }
    }
}
