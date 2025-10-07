using OnlineCoursePlatform.Application.Common.Extensions;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        public PermissionService(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }
        public async Task<bool> HasUserCoursePermissionAsync(Course course, string userId)
        {
            if (await _enrollmentRepository.IsUserEnrolledInCourseAsync(userId, course.Id))
                return true;

            if (course.IsCreatedBy(userId))
                return true;

            return false;
        }

        public bool UserHasPrivilegedRole(List<string> roles)
        {
            if (roles == null || roles.Count == 0)
                return false;

            return roles.Contains("Moderator");
        }
    }
}