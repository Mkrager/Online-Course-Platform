using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICourseRepository _courseRepository;
        public PermissionService(IEnrollmentRepository enrollmentRepository, ICourseRepository courseRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _courseRepository = courseRepository;
        }
        public async Task<bool> HasUserCoursePermissionAsync(Guid courseId, string userId)
        {
            if (await _enrollmentRepository.IsUserEnrolledInCourseAsync(userId, courseId))
                return true;

            if (await _courseRepository.IsUserCourseTeacherAsync(userId, courseId))
                return true;

            return false;
        }
    }
}