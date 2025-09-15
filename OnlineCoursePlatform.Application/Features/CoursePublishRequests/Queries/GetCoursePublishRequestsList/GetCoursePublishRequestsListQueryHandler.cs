using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Identity;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestsList
{
    public class GetCoursePublishRequestsListQueryHandler : IRequestHandler<GetCoursePublishRequestsListQuery, List<CoursePublishRequestsListVm>>
    {
        private readonly ICoursePublishRequestRepository _coursePublishRequestRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public GetCoursePublishRequestsListQueryHandler(ICoursePublishRequestRepository coursePublishRequestRepository, IMapper mapper, IUserService userService)
        {
            _coursePublishRequestRepository = coursePublishRequestRepository;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<List<CoursePublishRequestsListVm>> Handle(GetCoursePublishRequestsListQuery request, CancellationToken cancellationToken)
        {
            var coursePublishRequests = await _coursePublishRequestRepository.GetCoursePublishRequestsAsync(request.Status);

            var userIds = coursePublishRequests
                .SelectMany(c => new[] { c.RequestedBy, c.ApprovedBy })
                .Where(id => !string.IsNullOrEmpty(id))
                .Distinct();

            var userNamesMap = await _userService.GetUserNamesByIdsAsync(userIds);

            var mappedCoursePublishRequests = _mapper.Map<List<CoursePublishRequestsListVm>>(coursePublishRequests);

            for (int i = 0; i < mappedCoursePublishRequests.Count; i++)
            {
                var original = coursePublishRequests[i];
                var mapped = mappedCoursePublishRequests[i];

                mapped.RequestedName = userNamesMap.ContainsKey(original.RequestedBy)
                    ? userNamesMap[original.RequestedBy]
                    : original.RequestedBy;

                mapped.ApprovedName = !string.IsNullOrEmpty(original.ApprovedBy) && userNamesMap.ContainsKey(original.ApprovedBy)
                    ? userNamesMap[original.ApprovedBy]
                    : original.ApprovedBy;
            }

            return mappedCoursePublishRequests;
        }
    }
}