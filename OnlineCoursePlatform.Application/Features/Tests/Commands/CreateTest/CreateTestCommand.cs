using MediatR;
using OnlineCoursePlatform.Application.Common.Interfaces;

namespace OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest
{
    public class CreateTestCommand : IRequest<Guid>, IUserRequest
    {
        public Guid LessonId { get; set; }
        public string Title { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
        public List<string> UserRoles { get; set; } = new List<string>();

        public List<QuestionDto> Questions { get; set; } = default!;
    }
}
