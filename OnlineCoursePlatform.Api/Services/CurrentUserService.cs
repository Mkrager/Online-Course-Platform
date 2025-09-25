using OnlineCoursePlatform.Application.Contracts;

namespace OnlineCoursePlatform.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string UserId =>
            _contextAccessor.HttpContext.User.FindFirst("uid")?.Value;

        public string UserRoles =>
            _contextAccessor.HttpContext.User.FindFirst("role")?.Value;
    }
}
