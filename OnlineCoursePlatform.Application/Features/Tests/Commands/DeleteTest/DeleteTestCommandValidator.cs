using OnlineCoursePlatform.Application.Common.Filters;
using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Tests.Commands.DeleteTest
{
    public class DeleteTestCommandValidator : AccessValidator<DeleteTestCommand>
    {
        public DeleteTestCommandValidator(ICourseRepository courseRepository) : base(courseRepository)
        {
        }

        protected override async Task<bool> HasAccess(DeleteTestCommand model, CancellationToken token)
        {
            var course = await _courseRepository.GetCourseAsync(new CourseFilter() { TestId = model.Id });

            if (course == null)
                throw new NotFoundException(nameof(Course), model.Id);

            return await _courseRepository.IsUserCourseTeacherAsync(model.UserId, course.Id);
        }
    }
}
