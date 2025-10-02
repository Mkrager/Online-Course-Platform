using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Categories.Commands.CreateCategory;
using OnlineCoursePlatform.Application.UnitTests.Base;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Categories.Commands
{
    public class CreateCategoryCommandTest : TestBase
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        public CreateCategoryCommandTest()
        {
            _mockCategoryRepository = CategoryRepositoryMock.GetCategoryRepository();
        }

        [Fact]
        public async Task Handle_ValidCategory_AddedToCategoriesRepo()
        {
            var handler = new CreateCategoryCommandHandler(_mockCategoryRepository.Object, _mapper);

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
