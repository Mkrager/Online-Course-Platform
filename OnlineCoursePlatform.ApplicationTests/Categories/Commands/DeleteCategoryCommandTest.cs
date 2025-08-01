using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Categories.Commands.DeleteCategory;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using Shouldly;

namespace OnlineCoursePlatform.Application.UnitTests.Categories.Commands
{
    public class DeleteCategoryCommandTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;

        public DeleteCategoryCommandTest()
        {
            _mockCategoryRepository = CategoryRepositoryMock.GetCategoryRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Delete_Category_RemovesCategoryFromRepo()
        {
            var handler = new DeleteCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);
            await handler.Handle(new DeleteCategoryCommand() { Id = Guid.Parse("3f2a3a3e-27c9-4b65-bfb4-2b1e3d4b54ee") }, CancellationToken.None);

            var allCourses = await _mockCategoryRepository.Object.ListAllAsync();
            allCourses.Count.ShouldBe(1);
        }

    }
}
