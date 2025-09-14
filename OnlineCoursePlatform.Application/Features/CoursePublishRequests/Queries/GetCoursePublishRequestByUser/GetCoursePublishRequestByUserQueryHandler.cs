using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestsList;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestByUser
{
    public class GetCoursePublishRequestByUserQueryHandler : IRequestHandler<GetCoursePublishRequestByUserQuery, List<CoursePublishRequestsListVm>>
    {
        private readonly IMapper _mapper;
        private readonly ICoursePublishRequestRepository _coursePublishRequestRepository;
        public GetCoursePublishRequestByUserQueryHandler(IMapper mapper, ICoursePublishRequestRepository coursePublishRequestRepository)
        {
            _coursePublishRequestRepository = coursePublishRequestRepository;
            _mapper = mapper;
        }
        public async Task<List<CoursePublishRequestsListVm>> Handle(GetCoursePublishRequestByUserQuery request, CancellationToken cancellationToken)
        {
            var coursePublishRequests = await _coursePublishRequestRepository.GetCoursePublishRequestByUserIdAsync(request.UserId);
            return _mapper.Map<List<CoursePublishRequestsListVm>>(coursePublishRequests);
        }
    }
}
