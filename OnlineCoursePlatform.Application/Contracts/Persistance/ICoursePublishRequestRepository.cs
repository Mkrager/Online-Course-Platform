using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Contracts.Persistance
{
    public interface ICoursePublishRequestRepository
    {
        Task UpdateStatusAsync(CoursePublishRequest coursePublishRequest, CoursePublishStatus newStatus);
    }
}
