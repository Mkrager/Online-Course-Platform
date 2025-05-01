using OnlineCoursePlatform.Application.Contracts;
using System.Security.Claims;

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
    }
}
