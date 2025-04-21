using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Persistence.Repositories;

namespace OnlineCoursePlatform.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this
            IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OnlineCoursePlatformDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString
            ("OnlineCoursePlatformConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepositrory<>));

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITestRepository, TestRepository>();

            return services;
        }
    }
}
