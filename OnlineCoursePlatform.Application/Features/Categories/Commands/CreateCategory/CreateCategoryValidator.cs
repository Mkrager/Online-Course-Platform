using FluentValidation;
using OnlineCoursePlatform.Application.Contracts.Persistance;

namespace OnlineCoursePlatform.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        public CreateCategoryValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(e => e.Name)
                .MustAsync(CategoryNameUnique)
                .WithMessage("An category with the same name already exists.");
        }

        private async Task<bool> CategoryNameUnique(string name, CancellationToken token)
        {
            return await _categoryRepository.IsCategoryNameUniqueAsync(name);
        }

    }
}
