using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Contracts.Persistance
{
    public interface ITeacherApplicationRepository : IAsyncRepository<TeacherApplication>
    {
        Task UpdateStatusAsync(TeacherApplication teacherApplication, RequestStatus newStatus, string? rejectReason = null);
    }
}