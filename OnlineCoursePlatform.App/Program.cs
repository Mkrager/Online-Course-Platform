using OnlineCoursePlatform.App.Contracts;
using OnlineCoursePlatform.App.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddScoped<ICourseDataService, CourseDataService>();

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();
