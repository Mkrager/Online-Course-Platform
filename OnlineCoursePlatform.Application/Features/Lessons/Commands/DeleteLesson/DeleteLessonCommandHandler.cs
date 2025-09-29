using OnlineCoursePlatform.Application.Common.Handlers;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Lessons.Commands.DeleteLesson
{
    public class DeleteLessonCommandHandler : DeleteEntityCommandHandler<DeleteLessonCommand, Lesson>
    {
        public DeleteLessonCommandHandler(IAsyncRepository<Lesson> repository) : base(repository)
        {
        }

        protected override Guid GetEntityId(DeleteLessonCommand command) => command.Id;
    }
}
