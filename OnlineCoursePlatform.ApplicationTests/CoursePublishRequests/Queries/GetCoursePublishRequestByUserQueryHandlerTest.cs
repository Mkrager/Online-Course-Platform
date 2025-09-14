using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestByUser;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestsList;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.CoursePublishRequests.Queries
{
    public class GetCoursePublishRequestByUserQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICoursePublishRequestRepository> _mockCoursePublishRequestRepository;
        public GetCoursePublishRequestByUserQueryHandlerTest()
        {
            _mockCoursePublishRequestRepository = CoursePublishRequestRepositoryMock.GetCoursePublishRequest();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GeCoursePublishRequestList_ReturnsListOfCoursePublishRequests()
        {
            var handler = new GetCoursePublishRequestByUserQueryHandler(_mapper, _mockCoursePublishRequestRepository.Object);

            var result = await handler.Handle(new GetCoursePublishRequestByUserQuery() { UserId = "userId"}, CancellationToken.None);

            result.ShouldBeOfType<List<CoursePublishRequestsListVm>>();

            result.Count.ShouldBe(2);
        }
    }
}
