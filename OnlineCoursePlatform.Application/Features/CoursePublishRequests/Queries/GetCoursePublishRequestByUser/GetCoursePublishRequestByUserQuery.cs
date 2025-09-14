using MediatR;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestsList;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestByUser
{
    public class GetCoursePublishRequestByUserQuery : IRequest<List<CoursePublishRequestsListVm>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
