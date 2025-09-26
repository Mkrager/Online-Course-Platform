using OnlineCoursePlatform.App.ViewModels;
using System.Text.Json;
using System.Text;
using OnlineCoursePlatform.App.ViewModels.Authenticate;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using OnlineCoursePlatform.App.Infrastructure.BaseServices;
using OnlineCoursePlatform.App.Infrastructure.Api;

namespace OnlineCoursePlatform.App.Services
{
    public class AuthenticationService : BaseDataService, Contracts.IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse> Authenticate(AuthenticateRequest request)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("account/authenticate", content);

            var apiResponse = await HandleResponse<LoginResponse>(response);

            if (!apiResponse.IsSuccess || apiResponse.Data == null || string.IsNullOrEmpty(apiResponse.Data.Token))
                return new ApiResponse(apiResponse.StatusCode, apiResponse.ErrorText ?? "Authentication failed");

            var jwtToken = apiResponse.Data.Token;

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            var claims = token.Claims.ToList();
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(30)
                });

            _httpContextAccessor.HttpContext.Response.Cookies.Append("access_token", jwtToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(30)
            });

            return new ApiResponse(System.Net.HttpStatusCode.OK);
        }

        public async Task Logout()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("access_token");
        }

        public async Task<ApiResponse> Register(RegistrationRequest request)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("account/register", content);
            return await HandleResponse(response);
        }
    }
}