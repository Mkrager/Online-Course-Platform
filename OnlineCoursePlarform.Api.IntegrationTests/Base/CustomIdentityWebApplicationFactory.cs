using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Respawn;
using Respawn.Graph;
using Microsoft.Extensions.Configuration;
using OnlineCoursePlatform.Identity;

namespace OnlineCoursePlarform.Api.IntegrationTests.Base
{
    public class CustomIdentityWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private Respawner _respawner;
        private readonly IConfiguration _configuration;
        public CustomIdentityWebApplicationFactory()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(async services =>
            {
                services.RemoveAll(typeof(DbContextOptions<OnlineCoursePlatformIdentityDbContext>));

                services.AddDbContext<OnlineCoursePlatformIdentityDbContext>(options =>
                {
                    options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=OnlineCoursePlatformIdentityTestDb;Trusted_Connection=True;MultipleActiveResultSets=true");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<OnlineCoursePlatformIdentityDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    try
                    {
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
