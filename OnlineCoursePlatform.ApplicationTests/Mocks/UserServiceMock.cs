using Moq;
using OnlineCoursePlatform.Application.Contracts.Identity;
using OnlineCoursePlatform.Application.DTOs.User;

namespace OnlineCoursePlatform.Application.UnitTests.Mocks
{
    public class UserServiceMock
    {
        public static Mock<IUserService> GetUserService()
        {
            var mockService = new Mock<IUserService>();

            mockService.Setup(service => service.GetUserDetailsAsync(It.IsAny<string>()))
                .ReturnsAsync(new UserDetailsResponse()
                {
                    UserName = "Test",
                    Email = "test@gmai.com"
                });

            mockService.Setup(service => service.AssignRoleAsync(It.IsAny<string>(), It.IsAny<string>()));

            mockService.Setup(service => service.GetUserNamesByIdsAsync(It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync((IEnumerable<string> userIds) =>
                    userIds.ToDictionary(id => id, id => $"User_{id}"));

            return mockService;
        }
    }
}
