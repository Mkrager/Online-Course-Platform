using MediatR;
using OnlineCoursePlatform.Application.Common.Interfaces;

namespace OnlineCoursePlatform.Application.Features.User.Queries.GetUserDetails
{
    public class GetUserDetailsQuery : IRequest<UserDetailsVm>, IUserIdRequest
    {
        public string? UserId { get; set; }
    }
}
