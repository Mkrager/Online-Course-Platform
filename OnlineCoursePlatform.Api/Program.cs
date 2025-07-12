using Microsoft.AspNetCore.Identity;
using OnlineCoursePlatform.Api;
using OnlineCoursePlatform.Identity.Models;

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

        await OnlineCoursePlatform.Identity.Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
        await OnlineCoursePlatform.Identity.Seeds.DefaultSuperAdmin.SeedAsync(userManager, roleManager);
        await OnlineCoursePlatform.Identity.Seeds.DefaultBasicUser.SeedAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex}");
    }
}

app.Run();

public partial class Program { }