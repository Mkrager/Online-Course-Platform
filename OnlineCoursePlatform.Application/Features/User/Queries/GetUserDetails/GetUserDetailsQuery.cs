using MediatR;

namespace OnlineCoursePlatform.Application.Features.User.Queries.GetUserDetails
{
    public class GetUserDetailsQuery : IRequest<UserDetailsVm>
    {
        public string Id { get; set; } = string.Empty;
    }
}
