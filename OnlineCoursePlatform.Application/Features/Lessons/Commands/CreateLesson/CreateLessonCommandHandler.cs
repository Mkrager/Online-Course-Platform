using AutoMapper;
using OnlineCoursePlatform.Application.Common.Handlers;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Lessons.Commands.CreateLesson
{
    public class CreateLessonCommandHandler : CreateEntityCommandHandler<CreateLessonCommand, Lesson, Guid>
    {
        public CreateLessonCommandHandler(IAsyncRepository<Lesson> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override Guid BuildResponse(Lesson entity) => entity.Id;
    }
}
