using Moq;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestsList;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.UnitTests.Mocks
{
    public class RequestUserNamePopulatorMock
    {
        public static Mock<IRequestUserNamePopulator> GetRequestUserNamePopulator()
        {
            var mock = new Mock<IRequestUserNamePopulator>();

            mock.Setup(p => p.PopulateUserNamesAsync<CoursePublishRequest, CoursePublishRequestsListVm>(
                    It.IsAny<List<CoursePublishRequest>>()))
                .ReturnsAsync(new List<CoursePublishRequestsListVm>
                {
                    new() { RequestedName = "User 1", ProcessedName = "Admin 1" },
                    new() { RequestedName = "User 2", ProcessedName = "Admin 2" }
                });

            return mock;
        }
    }
}