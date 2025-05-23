﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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

            var result = await _repository.GetCoursesByUserId(_currentUserId);

            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
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

            var result = await _repository.GetCoursesByCategoryId(categoryId);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, c => Assert.Equal(categoryId, c.CategoryId));
            Assert.Equal(course1.Id, result.First().Id);
        }


        [Fact]
        public async Task GetAllWithCategoryAndLevel_ShouldReturnAllCourses()
        {
            var courseId = Guid.NewGuid();

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
                Id = courseId,
                CategoryId = categoryId,
                Title = "TestCourse",
                Level = level
            };

            _dbContext.Courses.Add(course);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetAllWithCategoryAndLevel();

            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
        }
    }
}