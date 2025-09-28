namespace OnlineCoursePlatform.Application.Common.Interfaces
{
    public interface IUserRequest : IUserIdRequest
    {
        List<string> UserRoles { get; set; }
    }
}