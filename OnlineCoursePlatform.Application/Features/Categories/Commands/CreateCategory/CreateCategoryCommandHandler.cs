//using AutoMapper;
//using MediatR;
//using OnlineCoursePlatform.Application.Contracts.Persistance;
//using OnlineCoursePlatform.Domain.Entities;

//namespace OnlineCoursePlatform.Application.Features.Categories.Commands.CreateCategory
//{
//    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
//    {
//        private readonly IMapper _mapper;
//        private readonly IAsyncRepository<Category> _categoryRepository;
//        public CreateCategoryCommandHandler(IMapper mapper, IAsyncRepository<Category> categoryRepository)
//        {
//            _categoryRepository = categoryRepository;
//            _mapper = mapper;
//        }

//        public Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
//        {
//        }
//    }
//}
