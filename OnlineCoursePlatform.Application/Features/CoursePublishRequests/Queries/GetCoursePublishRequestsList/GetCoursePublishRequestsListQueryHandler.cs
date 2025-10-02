using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Identity;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestsList
{
    public class GetCoursePublishRequestsListQueryHandler : IRequestHandler<GetCoursePublishRequestsListQuery, List<CoursePublishRequestsListVm>>
    {
        private readonly IRequestRepository<CoursePublishRequest> _requestRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public GetCoursePublishRequestsListQueryHandler(IRequestRepository<CoursePublishRequest> requestRepository, IMapper mapper, IUserService userService)
        {
            _requestRepository = requestRepository;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<List<CoursePublishRequestsListVm>> Handle(GetCoursePublishRequestsListQuery request, CancellationToken cancellationToken)
        {
            var coursePublishRequests = await _requestRepository.GetRequestsByStatusAsync(request.Status);

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