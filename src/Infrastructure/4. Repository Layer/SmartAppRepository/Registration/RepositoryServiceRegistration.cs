using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartAppRepository.Generic;
using SmartAppCore.Interfaces.Repository;
using SmartAppCore.Interfaces.Repository.Generic;

namespace SmartAppRepository.Registration
{
    public static class RepositoryServiceRegistration
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IMainRepositoryAsync<>), typeof(MainRepositoryAsync<>));
            services.AddScoped<ISearchRepository, SearchRepository>();
            services.AddScoped<IManagementRepository, ManagementRepository>();
            //services.AddScoped<IPropertyRepository, PropertyRepository>();
            return services;    
        }
    }
}