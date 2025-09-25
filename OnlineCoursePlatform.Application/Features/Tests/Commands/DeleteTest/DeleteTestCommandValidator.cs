using OnlineCoursePlatform.Application.Common.Filters;
using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Tests.Commands.DeleteTest
{
    public class DeleteTestCommandValidator : AccessValidator<DeleteTestCommand, ICourseRepository>
    {
        public DeleteTestCommandValidator(ICourseRepository service, string? errorMessage = null) 
            : base(service, errorMessage)
        {
        }

        protected override async Task<bool> HasAccessInternal(DeleteTestCommand model, CancellationToken token)
        {
            var course = await _service.GetCourseAsync(new CourseFilter() { TestId = model.Id });

            if (course == null)
                throw new NotFoundException(nameof(Course), model.Id);

            return await _service.IsUserCourseTeacherAsync(model.UserId, course.Id);
        }
    }
}