using FluentValidation;
using OnlineCoursePlatform.Application.Common.Filters;
using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Lessons.Commands.UpdateLesson
{
    public class UpdateLessonCommandValidator : AccessValidator<UpdateLessonCommand>
    {
        public UpdateLessonCommandValidator(ICourseRepository courseRepository) : base(courseRepository)
        {
            RuleFor(p => p.Title)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p.Order)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be grather than 0");


            RuleFor(p => p.Description)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }

        protected override async Task<bool> HasAccess(UpdateLessonCommand model, CancellationToken token)
        {
            var course = await _courseRepository.GetCourseAsync(new CourseFilter() { LessonId = model.Id });

            if (course == null)
                throw new NotFoundException(nameof(Course), model.Id);

            return await _courseRepository.IsUserCourseTeacherAsync(model.UserId, course.Id);
        }
    }
}
