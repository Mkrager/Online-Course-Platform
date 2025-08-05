using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Identity;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.User.Queries.GetUserDetails;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.Application.UnitTests.Mocks;

namespace OnlineCoursePlatform.Application.UnitTests.User.Queries
{
    public class GetUserDetailsQueryHandlerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<ICourseRepository> _mockCourseRepository;
        private readonly IMapper _mapper;
        public GetUserDetailsQueryHandlerTests()
        {
            _mockUserService = UserServiceMock.GetUserService();
            _mockCourseRepository = CourseRepositoryMock.GetCourseRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetUserDetails_ReturnsUserDetailsResponse()
        {
            var handler = new GetUserDetailsQueryHandler(_mockCourseRepository.Object, _mockUserService.Object, _mapper);

            var result = await handler.Handle(new GetUserDetailsQuery() { Id = "id" }, CancellationToken.None);

            Assert.NotNull(result);
        }
    }
}
