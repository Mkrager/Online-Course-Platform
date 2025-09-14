using AutoMapper;
using MediatR;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Identity;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestsList;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.CoursePublishRequests.Queries
{
    public class GetCoursePublishRequestsListQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICoursePublishRequestRepository> _mockCoursePublishRequestRepository;
        private readonly Mock<IUserService> _mockUserService;
        public GetCoursePublishRequestsListQueryHandlerTest()
        {
            _mockCoursePublishRequestRepository = CoursePublishRequestRepositoryMock.GetCoursePublishRequest();
            _mockUserService = UserServiceMock.GetUserService();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
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
