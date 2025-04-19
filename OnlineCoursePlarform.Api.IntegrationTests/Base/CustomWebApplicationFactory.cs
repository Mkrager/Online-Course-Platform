using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Moq;
using OnlineCoursePlatform.Persistence;
using Respawn;
using Respawn.Graph;
using System.Security.Claims;

namespace OnlineCoursePlarform.Api.IntegrationTests.Base
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private Respawner _respawner;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(async services =>
            {
                services.RemoveAll(typeof(DbContextOptions<OnlineCoursePlatformDbContext>));

                services.AddDbContext<OnlineCoursePlatformDbContext>(options =>
                {
                    options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=OnlineCoursePlatformTestDb;Trusted_Connection=True;MultipleActiveResultSets=true");
                });


                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<OnlineCoursePlatformDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    try
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();
                        var connection = context.Database.GetDbConnection();
                        await connection.OpenAsync();
                        _respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
                        {
                            TablesToIgnore = new Table[] { "__EFMigrationsHistory" },
                            WithReseed = true
                        });
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"An error occurred seeding the database. Error: {ex.Message}");
                    }
                }
            });
        }

        public HttpClient GetAnonymousClient()
        {
            return CreateClient();
        }
    }
}
