namespace OnlineCoursePlatform.Application.Contracts.Application
{
    public interface IPermissionService
    {
        Task<bool> HasUserCoursePermissionAsync(Guid courseId, string userId);
    }
}