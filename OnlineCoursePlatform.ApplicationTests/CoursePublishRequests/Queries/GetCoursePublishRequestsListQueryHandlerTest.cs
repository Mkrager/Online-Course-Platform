using Moq;
using OnlineCoursePlatform.Application.Contracts.Identity;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestsList;
using OnlineCoursePlatform.Application.UnitTests.Base;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using OnlineCoursePlatform.Domain.Entities;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.CoursePublishRequests.Queries
{
    public class GetCoursePublishRequestsListQueryHandlerTest : TestBase
    {
        private readonly Mock<IRequestRepository<CoursePublishRequest>> _mockCoursePublishRequestRepository;
        private readonly Mock<IUserService> _mockUserService;
        public GetCoursePublishRequestsListQueryHandlerTest()
        {
            _mockCoursePublishRequestRepository = CoursePublishRequestRepositoryMock.GetCoursePublishRequestRepository();
            _mockUserService = UserServiceMock.GetUserService();
        }

        [Fact]
        public async Task GeCoursePublishRequestList_ReturnsListOfCoursePublishRequests()
        {
            var handler = new GetCoursePublishRequestsListQueryHandler(_mockCoursePublishRequestRepository.Object, _mapper, _mockUserService.Object);

            var result = await handler.Handle(new GetCoursePublishRequestsListQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<CoursePublishRequestsListVm>>();

            result.Count.ShouldBe(2);
        }
    }
}
