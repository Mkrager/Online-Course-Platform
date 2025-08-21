using OnlineCoursePlarform.Api.IntegrationTests.Base;
using OnlineCoursePlatform.Application.DTOs.User;
using OnlineCoursePlatform.Application.Features.User.Commands.AssignRole;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit.Abstractions;

namespace OnlineCoursePlarform.Api.IntegrationTests.Controllers
{
    public class UserControllerTests : IClassFixture<CustomIdentityWebApplicationFactory<Program>>
    {
        private readonly CustomIdentityWebApplicationFactory<Program> _factory;
        private readonly ITestOutputHelper _output;
        public UserControllerTests(CustomIdentityWebApplicationFactory<Program> factory, ITestOutputHelper output)
        {
            _factory = factory;
            _output = output;
        }

        [Fact]
        public async Task GeUserDetailsById_ReturnsSuccessAndValidObject()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync($"/api/User/teacher/");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<UserDetailsResponse>(
                responseString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            Assert.NotNull(result);
            Assert.NotEmpty(result.UserName);
        }

        [Fact]
        public async Task AssignRole_ReturnsNoContent()
        {
            var client = _factory.GetAnonymousClient();

            var updateCourseCommand = new AssignRoleCommand
            {
                UserId = "7610e790-11fa-4a5c-8b90-0d5fa64dc59d",
                RoleName = "Default"
            };

            var content = new StringContent(
                JsonSerializer.Serialize(updateCourseCommand),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PutAsync("/api/user/assign-role", content);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
