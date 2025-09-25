using Moq;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.UnitTests.Mocks;

namespace OnlineCoursePlatform.Application.UnitTests.Base
{
    public abstract class AccessValidatorBaseTest : TestBase
    {
        protected readonly Mock<ICourseRepository> _mockCourseRepository;
        protected readonly Mock<IPermissionService> _mockPermissionService;

        protected AccessValidatorBaseTest()
        {
            _mockCourseRepository = CourseRepositoryMock.GetCourseRepository();
            _mockPermissionService = PermissiomServiceMock.GetPermissionService();
        }
    }
}