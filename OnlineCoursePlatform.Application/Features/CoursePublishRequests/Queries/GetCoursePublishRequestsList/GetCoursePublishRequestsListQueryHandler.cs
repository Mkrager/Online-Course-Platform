using MediatR;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestsList
{
    public class GetCoursePublishRequestsListQueryHandler : IRequestHandler<GetCoursePublishRequestsListQuery, List<CoursePublishRequestsListVm>>
    {
        private readonly IRequestRepository<CoursePublishRequest> _requestRepository;
        private readonly IRequestUserNamePopulator _requestUserNamePopulator;
        public GetCoursePublishRequestsListQueryHandler(IRequestRepository<CoursePublishRequest> requestRepository, IRequestUserNamePopulator requestUserNamePopulator)
        {
            _requestRepository = requestRepository;
            _requestUserNamePopulator = requestUserNamePopulator;
        }
        public async Task<List<CoursePublishRequestsListVm>> Handle(GetCoursePublishRequestsListQuery request, CancellationToken cancellationToken)
        {
            var coursePublishRequests = await _requestRepository.GetRequestsByStatusAsync(request.Status);

            return await _requestUserNamePopulator.PopulateUserNamesAsync<CoursePublishRequest, CoursePublishRequestsListVm>(coursePublishRequests);
        }
    }
}