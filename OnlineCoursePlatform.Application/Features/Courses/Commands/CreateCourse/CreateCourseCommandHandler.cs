using AutoMapper;
using OnlineCoursePlatform.Application.Common.Handlers;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandHandler : CreateEntityCommandHandler<CreateCourseCommand, Course, Guid>
    {
        public CreateCourseCommandHandler(IAsyncRepository<Course> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override Guid BuildResponse(Course entity) => entity.Id;
    }
}
