using Moq;
using OnlineCoursePlatform.Application.Contracts.Application;

namespace OnlineCoursePlatform.Application.UnitTests.Mocks
{
    public class PermissiomServiceMock
    {
        public static Mock<IPermissionService> GetPermissionService()
        {
            var mockRepository = new Mock<IPermissionService>();

            mockRepository.Setup(r => r.HasUserCoursePermissionAsync(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            mockRepository.Setup(r => r.UserHasPrivilegedRole(It.IsAny<List<string>>()))
                .Returns(true);

            return mockRepository;
        }
    }
}
