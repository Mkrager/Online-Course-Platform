using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.UnitTests.Mocks
{
    public class CategoryRepositoryMock
    {
        public static Mock<ICategoryRepository> GetCategoryRepository()
        {
            var categories = new List<Category>
            {
                new Category { Id = Guid.Parse("3f2a3a3e-27c9-4b65-bfb4-2b1e3d4b54ee"), Name = "TestCategory1" },
                new Category { Id = Guid.Parse("c1e9a0b2-5f3d-4427-8a3f-6db42c948ce4"), Name = "TestCategory2" }
            };

            var mockRepository = new Mock<ICategoryRepository>();

            mockRepository.Setup(r => r.ListAllAsync())
                .ReturnsAsync(categories);

            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => categories.FirstOrDefault(x => x.Id == id));

            mockRepository.Setup(r => r.AddAsync(It.IsAny<Category>()))
                .ReturnsAsync((Category category) =>
                {
                    categories.Add(category);
                    return category;
                });

            mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Category>()))
                .Callback((Category category) =>
                {
                    var oldCategory = categories.FirstOrDefault(x => x.Id == category.Id);
                    if (oldCategory != null)
                    {
                        oldCategory.Name = category.Name;
                    }
                });

            mockRepository.Setup(r => r.DeleteAsync(It.IsAny<Category>()))
                .Callback((Category category) => categories.Remove(category));

            mockRepository.Setup(repo => repo.GetCategoriesWithCoursesAsync()).ReturnsAsync(categories);

            mockRepository.Setup(repo => repo.IsCategoryNameUniqueAsync(It.IsAny<string>()))
                .ReturnsAsync((string name) => !categories.Any(c => c.Name == name));

            return mockRepository;
        }
    }
}
