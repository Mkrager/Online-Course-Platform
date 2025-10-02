using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Persistence.Interceptors;
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

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IUserAnswerRepository, UserAnswerRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped(typeof(IRequestRepository<>), typeof(RequestRepository<>));

            return services;
        }
    }
}