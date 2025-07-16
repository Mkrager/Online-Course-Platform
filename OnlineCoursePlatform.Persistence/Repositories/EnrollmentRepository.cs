using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Persistence.Repositories
{
    public class EnrollmentRepository : BaseRepository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(OnlineCoursePlatformDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> IsUserEnrolledInCourseAsync(string userId, Guid courseId)
        {
            var matches = await _dbContext.Enrollments.AnyAsync(x => x.StudentId == userId && x.CourseId == courseId);
            return matches;
        }
    }
}
