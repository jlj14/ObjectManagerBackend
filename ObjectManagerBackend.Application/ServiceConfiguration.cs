using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ObjectManagerBackend.Application.Services.AppObject;
using System.Reflection;

namespace ObjectManagerBackend.Application
{
    /// <summary>
    /// Configures the service dependencies of application layer
    /// </summary>
    public static class ServiceConfiguration
    {
        /// <summary>
        /// Adds the application dependencies to the service collection
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IAppObjectService, AppObjectService>();
            services.AddScoped<IAppObjectValidationService, AppObjectValidationService>();
            return services;
        }
    }
}
