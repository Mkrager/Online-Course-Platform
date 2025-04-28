using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.ViewModels;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Identity.Data;

namespace OnlineCoursePlatform.App.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public AuthenticationService(IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<ApiResponse<bool>> Authenticate(AuthenticateRequest request)
        {
            try
            {
                var authenticationRequest = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7275/api/Account/authenticate")
                {
                    Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(authenticationRequest);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var authenticationResponse = JsonSerializer.Deserialize<LoginResponse>(responseContent, _jsonOptions);


                    var jwtToken = authenticationResponse?.Token;

                    if (!string.IsNullOrEmpty(jwtToken))
                    {
                        _httpContextAccessor.HttpContext.Response.Cookies.Append("access_token", jwtToken, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict,
                            Expires = DateTime.UtcNow.AddDays(30)
                        });

                        return new ApiResponse<bool>(System.Net.HttpStatusCode.OK, true);
                    }
                }

                var errorContent = await response.Content.ReadAsStringAsync();

                var errorMessages = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent) ??
                                    new Dictionary<string, string> { { "error", errorContent } };

                return new ApiResponse<bool>(System.Net.HttpStatusCode.BadRequest, false, errorMessages.GetValueOrDefault("error"));
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(System.Net.HttpStatusCode.BadRequest, false, ex.Message);
            }
        }

        public string GetAccessToken()
        {
            return _httpContextAccessor.HttpContext.Request.Cookies["access_token"];
        }

        public Task Logout()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("access_token");
            return Task.CompletedTask;
        }

        public async Task<ApiResponse<bool>> Register(RegistrationRequest request)
        {
            try
            {
                var registerRequest = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7275/api/account/register")
                {
                    Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(registerRequest);

                if (response.IsSuccessStatusCode)
                {
                    return new ApiResponse<bool>(System.Net.HttpStatusCode.OK, true);
                }

                var errorContent = await response.Content.ReadAsStringAsync();

                var errorMessages = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent) ??
                                    new Dictionary<string, string> { { "error", errorContent } };

                return new ApiResponse<bool>(System.Net.HttpStatusCode.BadRequest, false, errorMessages.GetValueOrDefault("error"));
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(System.Net.HttpStatusCode.BadRequest, false, ex.Message);
            }
        }
    }
}
