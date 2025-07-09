using Microsoft.AspNetCore.Authentication.Cookies;
using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        //options.LoginPath = "/account/login";
        //options.AccessDeniedPath = "/account/denied";
    });

builder.Services.AddHttpClient();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICourseDataService, CourseDataService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserDataService, UserDataServcie>();
builder.Services.AddScoped<ICategoryDataService, CategoryDataService>();
builder.Services.AddScoped<ILevelDataService, LevelDataService>();
builder.Services.AddScoped<ILessonDataService, LessonDataService>();
builder.Services.AddScoped<ITestDataService, TestDataService>();
builder.Services.AddScoped<ITestAttemptDataService, TestAttemptDataService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();
