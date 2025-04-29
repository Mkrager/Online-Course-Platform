using MediatR;
using OnlineCoursePlatform.Application.Contracts.Identity;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.DTOs.User;

namespace OnlineCoursePlatform.Application.Features.User.Queries
{
    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserDetailsResponse>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserService _userService;
        public GetUserDetailsQueryHandler(ICourseRepository courseRepository, IUserService userService)
        {
            _courseRepository = courseRepository;
            _userService = userService;
        }
        public async Task<UserDetailsResponse> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserDetails(request.Id);

            user.Courses = await _courseRepository.GetCoursesByUserId(request.Id);

            return user;
        }
    }
}
