using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Contracts.Persistance
{
    public interface IEnrollmentRepository : IAsyncRepository<Enrollment>
    {
        Task<List<Enrollment>> GetStudentEnrollmentsWithCoursesAsync(string studentId);
        Task<bool> IsUserEnrolledInCourseAsync(string userId, Guid courseId);
    }
}
