using MediatR;
using OnlineCoursePlatform.Application.Common.Interfaces;

namespace OnlineCoursePlatform.Application.Features.Lessons.Queries.GetLessonDetail
{
    public class GetLessonDetailQuery : IRequest<LessonDetailVm>, IUserRequest
    {
        public Guid Id { get; set; }

        public string UserId { get; set; } = string.Empty;
        public string UserRoles { get; set; } = string.Empty;
    }
}
