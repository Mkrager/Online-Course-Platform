using FluentValidation;
using OnlineCoursePlatform.Application.Common.Filters;
using OnlineCoursePlatform.Application.Common.Validators;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Tests.Commands.UpdateTest
{
    public class UpdateTestCommandValidator : AccessValidator<UpdateTestCommand, ICourseRepository>
    {
        public UpdateTestCommandValidator(ICourseRepository service, string? errorMessage = null) 
            : base(service, errorMessage)
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Questions)
                .NotNull().WithMessage("Questions list is required.");

            RuleFor(p => p.Questions.Count)
                .GreaterThan(0)
                .WithMessage("There must be at least one question.")
                .When(p => p.Questions != null);


            RuleForEach(p => p.Questions)
                .SetValidator(new QuestionDtoValidator());
        }

        protected override async Task<bool> HasAccessInternal(UpdateTestCommand model, CancellationToken token)
        {
            var course = await _service.GetCourseAsync(new CourseFilter() { TestId = model.Id });

            if (course == null)
                throw new NotFoundException(nameof(Course), model.Id);

            return await _service.IsUserCourseTeacherAsync(model.UserId, course.Id);
        }
    }
}