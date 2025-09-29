using AutoMapper;
using OnlineCoursePlatform.Application.Common.Handlers;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.CoursePublishRequests.Commands.CreateCoursePublishRequest
{
    public class CreateCoursePublishRequestCommandHandler : CreateEntityCommandHandler<CreateCoursePublishRequestCommand, CoursePublishRequest, Guid>
    {
        public CreateCoursePublishRequestCommandHandler(IAsyncRepository<CoursePublishRequest> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override Guid BuildResponse(CoursePublishRequest entity) => entity.Id;
    }
}