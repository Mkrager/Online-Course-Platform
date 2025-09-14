using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineCoursePlatform.Application.Contracts;
using OnlineCoursePlatform.Domain.Entities;
using OnlineCoursePlatform.Persistence;
using OnlineCoursePlatform.Persistence.Repositories;

namespace OnlineCoursePlatfrom.Persistence.InregrationTests
{
    public class CourseRepositoryTests
    {
        private readonly OnlineCoursePlatformDbContext _dbContext;
        private readonly CourseRepository _repository;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly string _currentUserId;

        public CourseRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<OnlineCoursePlatformDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _currentUserId = "12300000-0000-0000-0000-000000000000";
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            _currentUserServiceMock.Setup(m => m.UserId).Returns(_currentUserId);

            _dbContext = new OnlineCoursePlatformDbContext(options, _currentUserServiceMock.Object);

            _repository = new CourseRepository(_dbContext);
        }

        [Fact]
        public async Task GetCoursesByUserId_WhenUserHasCourses_ReturnsUserCourses()
        {
            var courseId = Guid.NewGuid();

            var levelId = Guid.NewGuid();

            var level = new Level
            {
                Id = levelId,
                Name = "Test"
            };

            var categoryId = Guid.NewGuid();

            var category = new Category
            {
                Id = categoryId,
                Name = "Test",
            };

            var course = new Course
            {
                Id = courseId,
                LevelId = levelId,
                CategoryId = categoryId,
                Title = "TestCourse",
                Level = level,
                Category = category,
            };

            _dbContext.Courses.Add(course);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetCoursesByUserIdAsync(_currentUserId);

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetCoursesByCategoryId_ShouldReturnCoursesOfGivenCategory()
        {
            var categoryId = Guid.NewGuid();
            var anotherCategoryId = Guid.NewGuid();
            var level = new Level { Id = Guid.NewGuid(), Name = "Beginner" };

            var category = new Category { Id = categoryId, Name = "Development" };
            var anotherCategory = new Category { Id = anotherCategoryId, Name = "Business" };

            _dbContext.Categories.AddRange(category, anotherCategory);
            _dbContext.Levels.Add(level);

            var course1 = new Course
            {
                Id = Guid.NewGuid(),
                Title = "C# Basics",
                CategoryId = categoryId,
                Level = level
            };

            var course2 = new Course
            {
                Id = Guid.NewGuid(),
                Title = "ASP.NET Core",
                CategoryId = categoryId,
                Level = level
            };

            var course3 = new Course
            {
                Id = Guid.NewGuid(),
                Title = "Business Basics",
                CategoryId = anotherCategoryId,
                Level = level
            };

            _dbContext.Courses.AddRange(course1, course2, course3);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetCoursesByCategoryIdAsync(categoryId);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, c => Assert.Equal(categoryId, c.CategoryId));
            Assert.Equal(course1.Id, result.First().Id);
        }


        [Fact]
        public async Task GetAllCoursesWithCategoryAndLevel_ShouldReturnAllCourses()
        {
            var categoryId = Guid.NewGuid();

            var levelId = Guid.NewGuid();

            var level = new Level
            {
                Name = "Test",
                Id = levelId
            };

            var category = new Category
            {
                Id = categoryId,
                Name = "Test",
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            var course = new Course
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryId,
                Level = level,
                IsPublished = true
            };

            var course2 = new Course
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryId,
                Level = level,
                IsPublished = true
            };

            var course3 = new Course
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryId,
                Level = level,
                IsPublished = false
            };

            _dbContext.Courses.Add(course);
            _dbContext.Courses.Add(course2);
            _dbContext.Courses.Add(course3);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetCoursesWithCategoryAndLevelAsync();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetOnlyPublishedCoursesCoursesWithCategoryAndLevel_ShouldReturnOnlyPublishedCourses()
        {
            var categoryId = Guid.NewGuid();

            var levelId = Guid.NewGuid();

            var level = new Level
            {
                Name = "Test",
                Id = levelId
            };

            var category = new Category
            {
                Id = categoryId,
                Name = "Test",
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            var course = new Course
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryId,
                Level = level,
                IsPublished = true
            };

            var course2 = new Course
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryId,
                Level = level,
                IsPublished = true
            };

            var course3 = new Course
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryId,
                Level = level,
                IsPublished = false
            };

            _dbContext.Courses.Add(course);
            _dbContext.Courses.Add(course2);
            _dbContext.Courses.Add(course3);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetCoursesWithCategoryAndLevelAsync(true);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }


        [Fact]
        public async Task GetOnlyPublishedCoursesWithCategoryAndLevel_ShouldReturnAllCourses()
        {
            var categoryId = Guid.NewGuid();

            var levelId = Guid.NewGuid();

            var level = new Level
            {
                Name = "Test",
                Id = levelId
            };

            var category = new Category
            {
                Id = categoryId,
                Name = "Test",
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            var course = new Course
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryId,
                Level = level
            };

            var course2 = new Course
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryId,
                Level = level,
                IsPublished = true
            };

            var course3 = new Course
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryId,
                Title = "TestCourse",
                Level = level,
                IsPublished = true
            };

            _dbContext.Courses.Add(course);
            _dbContext.Courses.Add(course2);
            _dbContext.Courses.Add(course3);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetCoursesWithCategoryAndLevelAsync(true);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task UpdateIsPublishedAsync_ShouldUpdateIsPublishedProperty()
        {
            var course = new Course
            {
                Title = "Name",
                CreatedDate = DateTime.UtcNow,
                IsPublished = false
            };

            await _repository.AddAsync(course);

            await _repository.UpdateIsPublishedAsync(course, true);

            var updatedCourse = await _dbContext.Courses.FindAsync(course.Id);
            Assert.NotNull(updatedCourse);
            Assert.True(updatedCourse.IsPublished);
        }

        [Fact]
        public async Task GetCourseByIdWithCategoryAndLevelAsync_ShouldReturnCourse_WhenCourseExists()
        {
            var categoryId = Guid.NewGuid();

            var levelId = Guid.NewGuid();

            var level = new Level
            {
                Name = "Test",
                Id = levelId
            };

            var category = new Category
            {
                Id = categoryId,
                Name = "Test",
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            var courseId = Guid.NewGuid();

            var course = new Course
            {
                Id = courseId,
                Title = "Course",
                CreatedDate = DateTime.UtcNow,
                Category = category,
                Level = level
            };
            await _repository.AddAsync(course);

            var result = await _repository.GetCourseByIdWithCategoryAndLevelAsync(courseId);

            Assert.NotNull(result);
            Assert.Equal(course.Title, result.Title);
            Assert.NotNull(course.Category);
            Assert.NotNull(course.Level);
        }
    }
}