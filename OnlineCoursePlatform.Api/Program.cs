using Microsoft.AspNetCore.Identity;
using OnlineCoursePlatform.Api;
using OnlineCoursePlatform.Identity.Models;
using OnlineCoursePlatform.Identity.Seeds;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .ConfigureService()
    .ConfigurePipeline();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedAsync(userManager, roleManager);
        await DefaultSuperAdmin.SeedAsync(userManager, roleManager);
        await DefaultBasicUser.SeedAsync(userManager, roleManager);
        await DefaultTeacherUser.SeedAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex}");
    }
}

app.Run();

public partial class Program { }