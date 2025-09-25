namespace OnlineCoursePlatform.Application.Common.Interfaces
{
    public interface IUserRequest
    {
        public string UserId { get; set; }
        public string UserRoles { get; set; }
    }
}
