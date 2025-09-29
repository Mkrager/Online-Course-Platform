using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Common.Handlers;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Lessons.Commands.UpdateLesson
{
    public class UpdateLessonCommandHandler : UpdateEntityCommandHandler<UpdateLessonCommand, Lesson>
    {
        public UpdateLessonCommandHandler(IAsyncRepository<Lesson> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override Guid GetEntityId(UpdateLessonCommand command) => command.Id;
    }
}