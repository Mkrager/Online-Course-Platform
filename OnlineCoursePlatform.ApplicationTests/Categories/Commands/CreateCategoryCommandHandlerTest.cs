using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Application.Features.Categories.Commands.CreateCategory;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.ApplicationTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Categories.Commands
{
    public class CreateCategoryCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;

        public CreateCategoryCommandHandlerTest()
        {
            _mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidCategory_AddedToCategoriesRepo()
        {
            var handler = new CreateCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);

            var command = new CreateCategoryCommand() { Name = "dfgkldjklgjkd" };

            var result = await handler.Handle(command, CancellationToken.None);

            var allCategories = await _mockCategoryRepository.Object.ListAllAsync();
            allCategories.Count.ShouldBe(3);

            var createdCourse = allCategories.FirstOrDefault(a => a.Name == command.Name);
            createdCourse.ShouldNotBeNull();
            createdCourse.Name.ShouldBe(command.Name);
        }

        [Fact]
        public async Task Handle_ValidCategory_MustNotAddSecondResultToCategoriesRepo()
        {
            var handler = new CreateCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);

            var command = new CreateCategoryCommand() { Name = "dfgkldjklgjkd" };

            var result = await handler.Handle(command, CancellationToken.None);

            var exception = await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));

            var allCategories = await _mockCategoryRepository.Object.ListAllAsync();
            allCategories.Count.ShouldBe(3);

            var createdCourse = allCategories.FirstOrDefault(a => a.Name == command.Name);
            createdCourse.ShouldNotBeNull();
            createdCourse.Name.ShouldBe(command.Name);
        }

    }
}
