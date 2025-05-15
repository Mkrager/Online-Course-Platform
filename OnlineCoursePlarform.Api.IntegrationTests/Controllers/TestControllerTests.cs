using OnlineCoursePlarform.Api.IntegrationTests.Base;
using OnlineCoursePlatform.Application.Features.Courses.Commands.UpdateCourse;
using OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest;
using OnlineCoursePlatform.Application.Features.Tests.Commands.UpdateTest;
using OnlineCoursePlatform.Application.Features.Tests.Queries.GetTestDetail;
using OnlineCoursePlatform.Application.Features.Tests.Queries.GetUserTestsList;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit.Abstractions;

namespace OnlineCoursePlarform.Api.IntegrationTests.Controllers
{
    public class TestControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        public TestControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateTest_ReturnsSuccessAndValidResponse()
        {
            var client = _factory.GetAnonymousClient();

            var answers = new List<AnswerDto>() { new AnswerDto() { Text = "Test", IsCorrect = true } };

            var questions = new List<QuestionDto>() { new QuestionDto() { Text = "Test", Answers = answers } };

            var createTestCommand = new CreateTestCommand
            {
                Title = "TestCourse",
                LessonId = Guid.Parse("9c7f3d18-2c1e-4f37-9843-b25b6f1bfe49"),
                Questions = questions,
            };

            var content = new StringContent(
                JsonSerializer.Serialize(createTestCommand),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("/api/Test", content);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Guid>(responseString);

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetTestByLessonId_ReturnsSuccessAndNonEmptyResult()
        {
            var client = _factory.GetAnonymousClient();

            var lessonId = Guid.Parse("9c7f3d18-2c1e-4f37-9843-b25b6f1bfe49");

            var response = await client.GetAsync($"/api/test/lesson/{lessonId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<LessonTestListVm>>(responseString);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetTestWithQuestionAndAnswer_ReturnsSuccessAndNonEmptyResult()
        {
            var client = _factory.GetAnonymousClient();

            var id = Guid.Parse("1f5a4c21-2c9b-4b4e-bcb9-36b770a742d0");

            var response = await client.GetAsync($"/api/test/{id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TestDetailVm>(responseString);

            Assert.NotNull(result);
            Assert.IsType<TestDetailVm>(result);
        }

        [Fact]
        public async Task UpdateCourse_ReturnsSuccessAndValidResponse()
        {
            var client = _factory.GetAnonymousClient();

            var answer = new List<AnswerDto>()
            {
                new AnswerDto
                {
                    Text = "Test",
                    IsCorrect = true
                }
            };

            var question = new List<QuestionDto>()
            {
                new QuestionDto
                {
                    Text = "Test",
                    Answers = answer
                }
            };

            var updateTestCommand = new UpdateTestCommand()
            {
                Id = Guid.Parse("4a8c1a3f-7e1c-49d3-9bc1-1f8b38f1f3aa"),
                Title = "updTitle",
                Questions = question
            };

            var content = new StringContent(
                JsonSerializer.Serialize(updateTestCommand),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PutAsync($"/api/test/", content);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var updatedCourseResponse = await client.GetAsync($"/api/test/{updateTestCommand.Id}");

            updatedCourseResponse.EnsureSuccessStatusCode();

            var updatedCourse = JsonSerializer.Deserialize<TestDetailVm>(
                await updatedCourseResponse.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            Assert.Equal(updatedCourse.Title, updateTestCommand.Title);
        }


        [Fact]
        public async Task DeleteTest_ReturnsNoContent_WhenTestExists()
        {
            var client = _factory.GetAnonymousClient();

            var id = Guid.Parse("4a8c1a3f-7e1c-49d3-9bc1-1f8b38f1f3aa");

            var response = await client.DeleteAsync($"/api/test/{id}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
