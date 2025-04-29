using MediatR;
using OnlineCoursePlatform.Application.Contracts.Identity;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.DTOs.User;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.User.Queries
{
    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserDetailsResponse>
    {
        private readonly IAsyncRepository<Course> _courseRepository;
        private readonly IUserService _userService;
        public GetUserDetailsQueryHandler(IAsyncRepository<Course> courseRepository, IUserService userService)
        {
            _courseRepository = courseRepository;
            _userService = userService;
        }
        public async Task<UserDetailsResponse> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserDetails(request.Id);

            user.Courses = (await _courseRepository.ListAllAsync()).Where(x => x.CreatedBy == request.Id).ToList();

            return user;
        }
    }
}
