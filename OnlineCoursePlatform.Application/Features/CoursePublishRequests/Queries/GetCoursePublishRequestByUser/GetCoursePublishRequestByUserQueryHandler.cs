using MediatR;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestsList;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestByUser
{
    public class GetCoursePublishRequestByUserQueryHandler : IRequestHandler<GetCoursePublishRequestByUserQuery, List<CoursePublishRequestsListVm>>
    {
        private readonly IRequestRepository<CoursePublishRequest> _requestRepository;
        private readonly IRequestUserNamePopulator _requestUserNamePopulator;
        public GetCoursePublishRequestByUserQueryHandler(IRequestRepository<CoursePublishRequest> requestRepository, IRequestUserNamePopulator requestUserNamePopulator)
        {
            _requestRepository = requestRepository;
            _requestUserNamePopulator = requestUserNamePopulator;
        }
        public async Task<List<CoursePublishRequestsListVm>> Handle(GetCoursePublishRequestByUserQuery request, CancellationToken cancellationToken)
        {
            var coursePublishRequests = await _requestRepository.GetRequestByUserIdAsync(request.UserId);

            return await _requestUserNamePopulator.PopulateUserNamesAsync<CoursePublishRequest, CoursePublishRequestsListVm>(coursePublishRequests);
        }
    }
}
