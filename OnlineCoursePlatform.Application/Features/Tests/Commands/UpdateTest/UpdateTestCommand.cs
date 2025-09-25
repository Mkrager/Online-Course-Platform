using MediatR;
using OnlineCoursePlatform.Application.Common.Interfaces;
using OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest;

namespace OnlineCoursePlatform.Application.Features.Tests.Commands.UpdateTest
{
    public class UpdateTestCommand : IRequest, IUserRequest
    {
        public Guid Id { get; set; }
        public Guid LessonId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string UserRoles { get; set; } = string.Empty;

        public List<QuestionDto> Questions { get; set; } = default!;
    }
}
