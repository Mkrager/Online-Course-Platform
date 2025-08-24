using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestsList
{
    public class GetCoursePublishRequestsListQueryHandler : IRequestHandler<GetCoursePublishRequestsListQuery, List<CoursePublishRequestsListVm>>
    {
        private readonly IAsyncRepository<CoursePublishRequest> _coursePublishRequestRepository;
        private readonly IMapper _mapper;
        public GetCoursePublishRequestsListQueryHandler(IAsyncRepository<CoursePublishRequest> coursePublishRequestRepository, IMapper mapper)
        {
            _coursePublishRequestRepository = coursePublishRequestRepository;
            _mapper = mapper;
        }
        public async Task<List<CoursePublishRequestsListVm>> Handle(GetCoursePublishRequestsListQuery request, CancellationToken cancellationToken)
        {
            var coursePublishRequests = await _coursePublishRequestRepository.ListAllAsync();
            return _mapper.Map<List<CoursePublishRequestsListVm>>(coursePublishRequests);
        }
    }
}
