﻿using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.ApplicationTests.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<IAsyncRepository<Course>> GetCourseRepository()
        {
            var courses = new List<Course>
            {
                new Course { Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"), Title = "test", Description = "test", Price = 100, ThumbnailUrl = "test" },
                new Course { Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e2"), Title = "test2", Description = "test2", Price = 200, ThumbnailUrl = "test2" }

            };

            var mockRepository = new Mock<IAsyncRepository<Course>>();

            mockRepository.Setup(r => r.ListAllAsync())
                .ReturnsAsync(courses);

            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => courses.FirstOrDefault(x => x.Id == id));

            mockRepository.Setup(r => r.AddAsync(It.IsAny<Course>()))
                .ReturnsAsync((Course course) =>
                {
                    courses.Add(course);
                    return course;
                });

            mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Course>()))
                .Callback((Course course) =>
                {
                    var oldCourse = courses.FirstOrDefault(x => x.Id == course.Id);
                    if (oldCourse != null)
                    {
                        oldCourse.Title = course.Title;
                        oldCourse.Price = course.Price;
                        oldCourse.ThumbnailUrl = course.ThumbnailUrl;  
                        oldCourse.CategoryId = course.CategoryId;
                        oldCourse.Description = course.Description;
                    }
                });

            mockRepository.Setup(r => r.DeleteAsync(It.IsAny<Course>()))
                .Callback((Course course) => courses.Remove(course));

            return mockRepository;
        }
    }
}
