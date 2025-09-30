using AutoMapper;
using OnlineCoursePlatform.Application.Common.Handlers.Queries;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Lessons.Queries.GetLessonDetail
{
    public class GetLessonDetailQueryHandler : GetEntityByIdQueryHandler<GetLessonDetailQuery, Lesson, LessonDetailVm>
    {
        public GetLessonDetailQueryHandler(IAsyncRepository<Lesson> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override Guid GetEntityId(GetLessonDetailQuery query) => query.Id;
    }
}
