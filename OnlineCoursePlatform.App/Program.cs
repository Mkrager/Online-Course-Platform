using Microsoft.AspNetCore.Authentication.Cookies;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Middlewares;
using OnlineCoursePlatform.App.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddHttpClient();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<JwtHelper>();

builder.Services.AddScoped<ICourseDataService, CourseDataService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserDataService, UserDataServcie>();
builder.Services.AddScoped<ICategoryDataService, CategoryDataService>();
builder.Services.AddScoped<ILevelDataService, LevelDataService>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();
