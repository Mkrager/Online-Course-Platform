using MediatR;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestsList
{
    public class GetCoursePublishRequestsListQuery : IRequest<List<CoursePublishRequestsListVm>>
    {
    }
}
