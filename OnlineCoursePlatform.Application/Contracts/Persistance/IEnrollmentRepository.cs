using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Contracts.Persistance
{
    public interface IEnrollmentRepository : IAsyncRepository<Enrollment>
    {
        Task<bool> IsUserEnrolledInCourseAsync(string userId, Guid courseId);
    }
}
