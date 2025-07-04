using OnlineCoursePlarform.Api.IntegrationTests.Base;
using OnlineCoursePlatform.Application.Features.Courses.Commands.UpdateCourse;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCourseDetail;
using OnlineCoursePlatform.Application.Features.TestAttemps.Commands.EndAttempt;
using OnlineCoursePlatform.Application.Features.TestAttemps.Commands.StartAttempt;
using OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit.Abstractions;

namespace OnlineCoursePlarform.Api.IntegrationTests.Controllers
{
    public class TestAttemptControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        public TestAttemptControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task StartTest_ReturnsSuccessAndValidResponse()
        {
            var client = _factory.GetAnonymousClient();

            var startAttemptCommand = new StartAttemptCommand
            {
                StartTime = DateTime.UtcNow,
                IsCompleted = false,
                TestId = Guid.Parse("1f5a4c21-2c9b-4b4e-bcb9-36b770a742d0"),
                UserId = "SomeUserId"
            };

            var content = new StringContent(
                JsonSerializer.Serialize(startAttemptCommand),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("/api/TestAttempt", content);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Guid>(responseString);

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task EndTestAttempt_ReturnsSuccessAndValidResponse()
        {
            var client = _factory.GetAnonymousClient();

            var endAttemptCommand = new EndAttemptCommand
            {
                AttempId = Guid.Parse("d45f7a9e-3a01-4c64-9f86-cde3e55ebc36"),
                UserAnswerDto = new List<UserAnswerDto>()
                {
                    new UserAnswerDto
                    {
                        AnswerId = Guid.Parse("5cd711f0-cc43-4b7f-b6a3-d7f4c208b38a"),
                        UserId = "someUserId",
                        QuestionId = Guid.Parse("a1783ff1-7a2b-4d7a-84a5-c453be4c0f90"),
                        TestAttemptId = Guid.Parse("64f06d42-f0b3-4da7-60b2-08ddbaf7cc00"),
                        IsCorrect = true,
                    }
                }
            };

            var content = new StringContent(
                JsonSerializer.Serialize(endAttemptCommand),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PutAsync("/api/TestAttempt", content);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

    }
}
