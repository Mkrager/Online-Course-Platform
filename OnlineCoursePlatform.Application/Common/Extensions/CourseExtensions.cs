using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Common.Extensions
{
    public static class CourseExtensions
    {
        public static bool IsCreatedBy(this Course course, string userId)
            => course.CreatedBy == userId;
    }
}