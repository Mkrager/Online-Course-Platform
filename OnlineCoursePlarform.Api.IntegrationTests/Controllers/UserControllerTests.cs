using OnlineCoursePlarform.Api.IntegrationTests.Base;
using OnlineCoursePlatform.Application.DTOs.User;
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

            string userId = "7610e790-11fa-4a5c-8b90-0d5fa64dc59d";

            var response = await client.GetAsync($"/api/User/{userId}");

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

    }
}
