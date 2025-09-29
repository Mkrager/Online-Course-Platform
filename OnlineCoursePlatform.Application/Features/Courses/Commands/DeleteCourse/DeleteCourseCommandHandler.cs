using OnlineCoursePlatform.Application.Common.Handlers;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommandHandler : DeleteEntityCommandHandler<DeleteCourseCommand, Course>
    {
        public DeleteCourseCommandHandler(IAsyncRepository<Course> repository) : base(repository)
        {
        }

        protected override Guid GetEntityId(DeleteCourseCommand command) => command.Id;
    }
}
