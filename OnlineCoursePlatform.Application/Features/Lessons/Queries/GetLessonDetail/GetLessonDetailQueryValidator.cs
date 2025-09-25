using OnlineCoursePlatform.Application.Common.Filters;
using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Lessons.Queries.GetLessonDetail
{
    public class GetLessonDetailQueryValidator : AccessValidator<GetLessonDetailQuery, IPermissionService>
    {
        private readonly ICourseRepository _courseRepository;

        public GetLessonDetailQueryValidator(IPermissionService service, ICourseRepository courseRepository, string? errorMessage = null) 
            : base(service, errorMessage)
        {
            _courseRepository = courseRepository;
        }

        protected override async Task<bool> HasAccessInternal(GetLessonDetailQuery model, CancellationToken token)
        {
            var course = await _courseRepository.GetCourseAsync(new CourseFilter() { LessonId = model.Id });

            if (course == null)
                throw new NotFoundException(nameof(Course), model.Id);

            return await _service.HasUserCoursePermissionAsync(course.Id, model.UserId);
        }
    }
}