using AutoMapper;
using OnlineCoursePlatform.Application.Common.Handlers;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandHandler : UpdateEntityCommandHandler<UpdateCourseCommand, Course>
    {
        public UpdateCourseCommandHandler(IAsyncRepository<Course> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override Guid GetEntityId(UpdateCourseCommand command) => command.Id;
    }
}
