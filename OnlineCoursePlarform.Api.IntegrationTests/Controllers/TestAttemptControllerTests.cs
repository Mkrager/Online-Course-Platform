using OnlineCoursePlarform.Api.IntegrationTests.Base;
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
    }
}
