using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using OnlineCoursePlatform.Application.Contracts.Infrastructure;
using OnlineCoursePlatform.Infrastructure.PayPal;
using OnlineCoursePlatform.Infrastructure.Configuration;

namespace OnlineCoursePlatform.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IPayPalService, PayPalService>();

            services.AddHttpClient();
            services.AddMemoryCache();
            services.Configure<PayPalSettings>(configuration.GetSection("PayPal"));
            return services;
        }
    }
}
