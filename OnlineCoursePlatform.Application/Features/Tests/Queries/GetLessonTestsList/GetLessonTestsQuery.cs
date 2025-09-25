using MediatR;
using OnlineCoursePlatform.Application.Common.Interfaces;

namespace OnlineCoursePlatform.Application.Features.Tests.Queries.GetUserTestsList
{
    public class GetLessonTestsQuery : IRequest<List<LessonTestListVm>>, IUserRequest
    {
        public Guid LessonId { get; set; }

        public string UserId { get; set; } = string.Empty;
        public string UserRoles { get; set; } = string.Empty;
    }
}
