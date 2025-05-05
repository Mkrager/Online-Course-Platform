using OnlineCoursePlarform.Api.IntegrationTests.Base;
using OnlineCoursePlatform.Application.Features.Lessons.Commands.CreateLesson;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit.Abstractions;

namespace OnlineCoursePlarform.Api.IntegrationTests.Controllers
{
    public class LessonControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly ITestOutputHelper _output;
        public LessonControllerTests(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output)
        {
            _factory = factory;
            _output = output;
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
            _output.WriteLine(response.ToString());

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            _output.WriteLine(responseString);
            var result = JsonSerializer.Deserialize<Guid>(responseString);

            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result);
        }

    }
}
