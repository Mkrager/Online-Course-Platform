using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OnlineCoursePlatform.Application.Behaviours;
using OnlineCoursePlatform.Application.Contracts.Application;
using OnlineCoursePlatform.Application.Services;
using System.Reflection;

namespace OnlineCoursePlatform.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection
            services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<ICheckoutService, CheckoutService>();
            services.AddScoped<IPermissionService, PermissionService>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
