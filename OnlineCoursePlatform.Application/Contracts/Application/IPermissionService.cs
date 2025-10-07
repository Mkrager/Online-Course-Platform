using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Contracts.Application
{
    public interface IPermissionService
    {
        Task<bool> HasUserCoursePermissionAsync(Course course, string userId);
        bool UserHasPrivilegedRole(List<string> roles);
    }
}