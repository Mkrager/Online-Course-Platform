using Moq;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestByUser;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestsList;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using OnlineCoursePlatform.Domain.Entities;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.CoursePublishRequests.Queries
{
    public class GetCoursePublishRequestByUserQueryHandlerTest
    {
        private readonly Mock<IRequestRepository<CoursePublishRequest>> _mockCoursePublishRequestRepository;
        private readonly Mock<IRequestUserNamePopulator> _mockRequestUserNamePopulator;
        public GetCoursePublishRequestByUserQueryHandlerTest()
        {
            _mockCoursePublishRequestRepository = CoursePublishRequestRepositoryMock.GetCoursePublishRequestRepository();
            _mockRequestUserNamePopulator = RequestUserNamePopulatorMock.GetRequestUserNamePopulator();
        }

        [Fact]
        public async Task GeCoursePublishRequestList_ReturnsListOfCoursePublishRequests()
        {
            var handler = new GetCoursePublishRequestByUserQueryHandler(_mockCoursePublishRequestRepository.Object, _mockRequestUserNamePopulator.Object);

            var result = await handler.Handle(new GetCoursePublishRequestByUserQuery() { UserId = "userId"}, CancellationToken.None);

            result.ShouldBeOfType<List<CoursePublishRequestsListVm>>();

            result.Count.ShouldBe(2);
        }
    }
}
