using MediatR;
using OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest;

namespace OnlineCoursePlatform.Application.Features.Tests.Commands.UpdateTest
{
    public class UpdateTestCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public List<QuestionDto> Questions { get; set; } = default!;
    }
}
