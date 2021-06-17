using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartAppDataAccess.Interfaces;
using SmartAppCore.Interfaces.Persistence;

namespace SmartAppDataAccess.Registration
{
    public static class DataAccessServiceRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            //TODO: Keep configuration input to get connection strings for CRUD operations - RDBS connection
            services.AddScoped<INoSQLSearchDataAccess, NoSQLSearchDataAccess>();
            services.AddScoped<INoSQLManagementDataAccess, NoSQLManagementDataAccess>();
            services.AddScoped<INoSQLPropertyDataAccess, NoSQLPropertyDataAccess>(); 
            return services;    
        }
    }
}