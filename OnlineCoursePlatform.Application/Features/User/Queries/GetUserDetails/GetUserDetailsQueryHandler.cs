using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Identity;

namespace OnlineCoursePlatform.Application.Features.User.Queries.GetUserDetails
{
    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserDetailsVm>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public GetUserDetailsQueryHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<UserDetailsVm> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserDetailsAsync(request.UserId);

            return _mapper.Map<UserDetailsVm>(user);
        }
    }
}
