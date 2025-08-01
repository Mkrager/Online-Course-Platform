using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.UnitTests.Mocks
{
    public class LevelRepositoryMock
    {
        public static Mock<IAsyncRepository<Level>> GetLevelRepository()
        {
            var levels = new List<Level>
            {
                new Level { Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"), Name = "test" },
                new Level { Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e2"), Name = "test" }

            };

            var mockRepository = new Mock<IAsyncRepository<Level>>();

            mockRepository.Setup(r => r.ListAllAsync())
                .ReturnsAsync(levels);

            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => levels.FirstOrDefault(x => x.Id == id));

            mockRepository.Setup(r => r.AddAsync(It.IsAny<Level>()))
                .ReturnsAsync((Level level) =>
                {
                    levels.Add(level);
                    return level;
                });

            mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Level>()))
                .Callback((Level level) =>
                {
                    var oldLevel = levels.FirstOrDefault(x => x.Id == level.Id);
                    if (oldLevel != null)
                    {
                        oldLevel.Name = level.Name;
                    }
                });

            mockRepository.Setup(r => r.DeleteAsync(It.IsAny<Level>()))
                .Callback((Level level) => levels.Remove(level));

            return mockRepository;
        }
    }
}
