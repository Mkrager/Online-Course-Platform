using MediatR;
using OnlineCoursePlatform.Domain.Enums;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestsList
{
    public class GetCoursePublishRequestsListQuery : IRequest<List<CoursePublishRequestsListVm>>
    {
        public RequestStatus? Status { get; set; }
    }
}
