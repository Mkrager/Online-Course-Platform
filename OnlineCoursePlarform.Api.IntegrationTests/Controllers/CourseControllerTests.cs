﻿using OnlineCoursePlarform.Api.IntegrationTests.Base;
using OnlineCoursePlatform.Application.Features.Courses.Commands.CreateCourse;
using OnlineCoursePlatform.Application.Features.Courses.Commands.UpdateCourse;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCourseDetail;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesByCategory;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit.Abstractions;

namespace OnlineCoursePlarform.Api.IntegrationTests.Controllers
{
    public class CourseControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        public CourseControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllCourses_ReturnsSuccessAndNonEmptyList()
        {
            var client = _factory.GetAnonymousClient();
            var response = await client.GetAsync("/api/Course");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<CourseListVm>>(responseString);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task GetCoursesByCategoryId_ReturnsSuccessAndNonEmptyList()
        {
            var client = _factory.GetAnonymousClient();

            var categoryId = Guid.Parse("6f4c7e59-74c7-41c5-9fa7-4b75b7d9f3a3");

            var ressponse = await client.GetAsync($"/api/Course/by-category/{categoryId}");

            ressponse.EnsureSuccessStatusCode();

            var responseString = await ressponse.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<CoursesByCategoryVm>>(responseString);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task GeCourseById_ReturnsSuccessAndValidObject()
        {
            var client = _factory.GetAnonymousClient();

            Guid courseId = Guid.Parse("7e1e9e74-905f-4ad6-8f8d-26ab9dd98ec1");

            var response = await client.GetAsync($"/api/Course/{courseId}");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<CourseDetailVm>(
                responseString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            Assert.NotNull(result);
            Assert.Equal(courseId, result.Id);
            Assert.NotEmpty(result.Title);
        }

        [Fact]
        public async Task CreateCourse_ReturnsSuccessAndValidResponse()
        {
            var client = _factory.GetAnonymousClient();

            var createCourseCommand = new CreateCourseCommand
            {
                Title = "TestCourse",
                Description = "TestCourseDescriptionTestCourseDescriptionTestCourseDescriptionTestCourseDescriptionTestCourseDescriptionTestCourseDescriptionTestCourseDescriptionTestCourseDescriptionTestCourseDescription",
                ThumbnailUrl = "TestThumbnailUrl",
                Price = 1000,
                CategoryId = Guid.Parse("6f4c7e59-74c7-41c5-9fa7-4b75b7d9f3a3"),
                LevelId = Guid.Parse("03e986cf-2784-4096-b130-2762c2018777")

            };

            var content = new StringContent(
                JsonSerializer.Serialize(createCourseCommand),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("/api/Course", content);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Guid>(responseString);

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var createdCourseResponse = await client.GetAsync($"/api/Course/{result}");
            createdCourseResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task UpdateCourse_ReturnsSuccessAndValidResponse()
        {
            var client = _factory.GetAnonymousClient();

            var updateCourseCommand = new UpdateCourseCommand
            {
                Id = Guid.Parse("7e1e9e74-905f-4ad6-8f8d-26ab9dd98ec1"),
                Title = "UpdatedCourse",
                Description = "UpdatedCourseDescriptionUpdatedCourseDescriptionUpdatedCourseDescriptionUpdatedCourseDescriptionUpdatedCourseDescriptionUpdatedCourseDescriptionUpdatedCourseDescriptionUpdatedCourseDescription",
                Price = 2000,
                ThumbnailUrl = "TestThumbnailUrl",
                CategoryId = Guid.Parse("6f4c7e59-74c7-41c5-9fa7-4b75b7d9f3a3")
            };

            var content = new StringContent(
                JsonSerializer.Serialize(updateCourseCommand),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PutAsync("/api/Course", content);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var updatedCourseResponse = await client.GetAsync($"/api/Course/{updateCourseCommand.Id}");

            updatedCourseResponse.EnsureSuccessStatusCode();

            var updatedCourse = JsonSerializer.Deserialize<CourseDetailVm>(
                await updatedCourseResponse.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            Assert.Equal("UpdatedCourse", updatedCourse.Title);
        }


        [Fact]
        public async Task DeleteCourse_ReturnsNoContent_WhenCourseExists()
        {
            var client = _factory.GetAnonymousClient();

            Guid courseId = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8");

            var response = await client.DeleteAsync($"/api/Course/{courseId}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

    }
}
