using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Application.Features.Account.Commands.Registration;
using OnlineCoursePlatform.Application.Features.Categories.Commands.CreateCategory;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Categories.Commands
{
    public class CreateCategoryCommandTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;

        public CreateCategoryCommandTest()
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
        public async void Validator_ShouldHaveError_WhenNameEmpty()
        {
            var validator = new CreateCategoryValidator(_mockCategoryRepository.Object);
            var query = new CreateCategoryCommand
            {
                Name = ""
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Name");
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenNameNotUnique()
        {
            var validator = new CreateCategoryValidator(_mockCategoryRepository.Object);
            var query = new CreateCategoryCommand
            {
                Name = "TestCategory1"
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Name");
        }
    }
}
