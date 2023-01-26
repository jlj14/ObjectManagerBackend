using Microsoft.Extensions.DependencyInjection;
using ObjectManagerBackend.Domain.Contracts.Repositories;
using ObjectManagerBackend.Infrastructure.Persistence;
using ObjectManagerBackend.Infrastructure.Repositories;

namespace ObjectManagerBackend.Infrastructure
{
    /// <summary>
    /// Configures the service dependencies of infrastructure layer
    /// </summary>
    public static class ServiceConfiguration
    {
        /// <summary>
        /// Adds the infrastructure dependencies to the service collection
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<InMemoryContext>();
            services.AddSingleton<IAppObjectRepository, AppObjectRepository>();
            return services;
        }
    }
}
