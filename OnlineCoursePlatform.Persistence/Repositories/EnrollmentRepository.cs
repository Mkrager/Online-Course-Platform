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

        public async Task<List<Enrollment>> GetEnrollmentsByStudentIdWithCoursesAsync(string studentId)
        {
            return await _dbContext.Enrollments
                .Include(x => x.Course)
                    .ThenInclude(x => x.Level)
                .Include(x => x.Course)
                    .ThenInclude(x => x.Category)
                .Where(s => s.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<bool> IsUserEnrolledInCourseAsync(string userId, Guid courseId)
        {
            var matches = await _dbContext.Enrollments.AnyAsync(x => x.StudentId == userId && x.CourseId == courseId);
            return matches;
        }
    }
}
