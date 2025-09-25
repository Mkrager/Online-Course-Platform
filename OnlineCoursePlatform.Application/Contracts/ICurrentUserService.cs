namespace OnlineCoursePlatform.Application.Contracts
{
    public interface ICurrentUserService
    {
        public string UserId { get; }
        public List<string> UserRoles { get; }
    }
}
