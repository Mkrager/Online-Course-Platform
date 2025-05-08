using MediatR;

namespace OnlineCoursePlatform.Application.Features.Lessons.Queries.GetLessonDetail
{
    public class GetLessonDetailQuery : IRequest<LessonDetailVm>
    {
        public Guid Id { get; set; }
    }
}
