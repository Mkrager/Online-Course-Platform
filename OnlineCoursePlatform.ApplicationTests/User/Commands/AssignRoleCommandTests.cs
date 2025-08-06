using AutoMapper;
using MediatR;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Identity;
using OnlineCoursePlatform.Application.Features.Courses.Commands.CreateCourse;
using OnlineCoursePlatform.Application.Features.User.Commands.AssignRole;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.User.Commands
{
    public class AssignRoleCommandTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly IMapper _mapper;
        public AssignRoleCommandTests()
        {
            _mockUserService = UserServiceMock.GetUserService();
        }

        [Fact]
        public async Task Should_Assign_Role_Successfully()
        {
            var handler = new AssignRoleCommandHandler(_mockUserService.Object);

            var command = new AssignRoleCommand
            {
                UserId = "id",
                RoleName = "name"
            };

            var result = await handler.Handle(command, CancellationToken.None);

            result.ShouldBe(Unit.Value);
        }
    }
}
