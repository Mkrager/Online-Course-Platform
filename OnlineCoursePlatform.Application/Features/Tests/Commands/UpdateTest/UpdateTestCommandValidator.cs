﻿using FluentValidation;
using OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest;

namespace OnlineCoursePlatform.Application.Features.Tests.Commands.UpdateTest
{
    public class UpdateTestCommandValidator : AbstractValidator<UpdateTestCommand>
    {
        public UpdateTestCommandValidator()
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

    }
}
