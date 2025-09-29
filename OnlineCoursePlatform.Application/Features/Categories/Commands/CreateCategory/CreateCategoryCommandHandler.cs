using AutoMapper;
using OnlineCoursePlatform.Application.Common.Handlers.Commands;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Categories.Commands.CreateCategory
{ 
    public class CreateCategoryCommandHandler : CreateEntityCommandHandler<CreateCategoryCommand, Category, Guid>
    {
        public CreateCategoryCommandHandler(IAsyncRepository<Category> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override Guid BuildResponse(Category entity) => entity.Id;
    }
}