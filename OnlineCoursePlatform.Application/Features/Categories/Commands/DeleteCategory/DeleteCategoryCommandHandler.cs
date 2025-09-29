using OnlineCoursePlatform.Application.Common.Handlers.Commands;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : DeleteEntityCommandHandler<DeleteCategoryCommand, Category>
    {
        public DeleteCategoryCommandHandler(IAsyncRepository<Category> repository) : base(repository)
        {
        }

        protected override Guid GetEntityId(DeleteCategoryCommand command) => command.Id;
    }
}
