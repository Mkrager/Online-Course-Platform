using OnlineCoursePlarform.Api.IntegrationTests.Base;
using OnlineCoursePlatform.Application.Features.Lessons.Commands.CreateLesson;
using OnlineCoursePlatform.Application.Features.Lessons.Commands.UpdateLesson;
using OnlineCoursePlatform.Application.Features.Lessons.Queries.GetCourseLessonsList;
using OnlineCoursePlatform.Application.Features.Lessons.Queries.GetLessonDetail;
using System.Net;
using System.Text;
using System.Text.Json;

namespace OnlineCoursePlarform.Api.IntegrationTests.Controllers
{
    public class LessonControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        public LessonControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateLesson_ReturnsSuccessAndValidResponse()
        {
            var client = _factory.GetAnonymousClient();

            var createLessonCommand = new CreateLessonCommand
            {
                Description = "someDescsomeDescsomeDescsomeDescsomeDesc",
                Title = "someTitle",
                Order = 1,
                CourseId = Guid.Parse("7e1e9e74-905f-4ad6-8f8d-26ab9dd98ec1")
            };

            var content = new StringContent(
                JsonSerializer.Serialize(createLessonCommand),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("/api/lesson", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<Guid>(responseString);

            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public async Task GetLessonById_RuturnsSuccessAndValidObject()
        {
            var client = _factory.GetAnonymousClient();

            Guid lessonId = Guid.Parse("9c7f3d18-2c1e-4f37-9843-b25b6f1bfe49");

            var response = await client.GetAsync($"/api/Lesson/{lessonId}");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<LessonDetailVm>(
                responseString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            Assert.NotNull(result);
            Assert.Equal(result.Id, Guid.Parse("9c7f3d18-2c1e-4f37-9843-b25b6f1bfe49"));
        }

        [Fact]
        public async Task GetCourseLessons_ReturnsSuccessAndValidObject()
        {
            var client = _factory.GetAnonymousClient();

            Guid courseId = Guid.Parse("7e1e9e74-905f-4ad6-8f8d-26ab9dd98ec1");

            var response = await client.GetAsync($"/api/lesson/by-course/{courseId}");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<CourseLessonListVm>>(
                responseString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task UpdateLesson_ReturnsNoContent_WhemLessonUpdated()
        {
            var client = _factory.GetAnonymousClient();

            Guid lessonId = Guid.Parse("9c7f3d18-2c1e-4f37-9843-b25b6f1bfe49");

            var updateLessonCommand = new UpdateLessonCommand
            {
                Id = lessonId,
                Title = "Upd",
                Description = "UpdUpdUpdUpdUpdUpdUpdUpdUpdUpdUpdUpdUpdUpdUpdUpdUpdUpdUpdUpdUpdUpdUpdUpdUpdUpdUpdUpdUpdUpdUpdUpd",
                Order = 1,
            };

            var content = new StringContent(
                JsonSerializer.Serialize(updateLessonCommand),
                Encoding.UTF8,
                "application/json"
                );

            var response = await client.PutAsync($"/api/lesson", content);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var updateLessonResponse = await client.GetAsync($"/api/Lesson/{lessonId}");

            updateLessonResponse.EnsureSuccessStatusCode();


            var updatedLesson = JsonSerializer.Deserialize<LessonDetailVm>(
                await updateLessonResponse.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.Equal("Upd", updatedLesson.Title);
        }

        [Fact]
        public async Task DeleteLesson_ReturnsNoContent_WhenLessonExists()
        {
            var client = _factory.GetAnonymousClient();

            Guid lessonId = Guid.Parse("2e8b13d5-4c5e-4f4b-9387-8e19c844dbe9");

            var response = await client.DeleteAsync($"api/lesson/{lessonId}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
