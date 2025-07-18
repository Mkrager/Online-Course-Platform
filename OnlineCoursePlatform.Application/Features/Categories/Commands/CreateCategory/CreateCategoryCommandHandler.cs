﻿using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public CreateCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var @category = _mapper.Map<Category>(request);

            @category = await _categoryRepository.AddAsync(category);

            return @category.Id;
        }
    }
}
