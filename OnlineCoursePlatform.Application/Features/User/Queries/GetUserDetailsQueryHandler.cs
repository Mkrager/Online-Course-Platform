using AutoMapper;
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
        private readonly IMapper _mapper;
        public GetUserDetailsQueryHandler(ICourseRepository courseRepository, IUserService userService, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<UserDetailsResponse> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserDetails(request.Id);

            var courses = await _courseRepository.GetCoursesByUserId(request.Id);

            user.Courses = _mapper.Map<List<UserCourseVm>>(courses);

            return user;
        }
    }
}
