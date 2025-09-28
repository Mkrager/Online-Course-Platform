using MediatR;
using OnlineCoursePlatform.Application.Common.Interfaces;
using OnlineCoursePlatform.Application.Contracts;

namespace OnlineCoursePlatform.Application.Behaviours
{
    public class UserContextBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, IUserIdRequest
    {
        private readonly ICurrentUserService _currentUserService;

        public UserContextBehavior(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            request.UserId = _currentUserService.UserId;

            if (request is IUserRequest withRoles)
            {
                withRoles.UserRoles = _currentUserService.UserRoles;
            }

            return await next();
        }
    }
}