using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Identity;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestsList;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestByUser
{
    public class GetCoursePublishRequestByUserQueryHandler : IRequestHandler<GetCoursePublishRequestByUserQuery, List<CoursePublishRequestsListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IRequestRepository<CoursePublishRequest> _requestRepository;
        private readonly IUserService _userService;
        public GetCoursePublishRequestByUserQueryHandler(IMapper mapper, IRequestRepository<CoursePublishRequest> requestRepository, IUserService userService)
        {
            _requestRepository = requestRepository;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<List<CoursePublishRequestsListVm>> Handle(GetCoursePublishRequestByUserQuery request, CancellationToken cancellationToken)
        {
            var coursePublishRequests = await _requestRepository.GetRequestByUserIdAsync(request.UserId);
            var userIds = coursePublishRequests
                .SelectMany(c => new[] { c.RequestedBy, c.ProcessedBy })
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

                mapped.ProcessedName = !string.IsNullOrEmpty(original.ProcessedBy) && userNamesMap.ContainsKey(original.ProcessedBy)
                    ? userNamesMap[original.ProcessedBy]
                    : original.ProcessedBy;
            }

            return mappedCoursePublishRequests;
        }
    }
}
