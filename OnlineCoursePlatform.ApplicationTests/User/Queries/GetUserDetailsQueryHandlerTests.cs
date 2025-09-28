using Moq;
using OnlineCoursePlatform.Application.Contracts.Identity;
using OnlineCoursePlatform.Application.Features.User.Queries.GetUserDetails;
using OnlineCoursePlatform.Application.UnitTests.Base;
using OnlineCoursePlatform.Application.UnitTests.Mocks;

namespace OnlineCoursePlatform.Application.UnitTests.User.Queries
{
    public class GetUserDetailsQueryHandlerTests : TestBase
    {
        private readonly Mock<IUserService> _mockUserService;
        public GetUserDetailsQueryHandlerTests()
        {
            _mockUserService = UserServiceMock.GetUserService();
        }

        [Fact]
        public async Task GetUserDetails_ReturnsUserDetailsResponse()
        {
            var handler = new GetUserDetailsQueryHandler(_mockUserService.Object, _mapper);

            var result = await handler.Handle(new GetUserDetailsQuery() { UserId = "id" }, CancellationToken.None);

            Assert.NotNull(result);
        }
    }
}
