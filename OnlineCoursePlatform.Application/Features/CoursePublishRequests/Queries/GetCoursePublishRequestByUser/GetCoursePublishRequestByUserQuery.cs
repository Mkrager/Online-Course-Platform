using MediatR;
using OnlineCoursePlatform.Application.Common.Interfaces;
using OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestsList;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Queries.GetCoursePublishRequestByUser
{
    public class GetCoursePublishRequestByUserQuery : IRequest<List<CoursePublishRequestsListVm>>, IUserIdRequest
    {
        public string? UserId { get; set; }
    }
}