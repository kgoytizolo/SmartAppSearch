using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SmartAppService.Interfaces;
using SmartAppService.Validations;
using SmartAppRepository;
using SmartAppCore.Interfaces.Repository;
using SmartAppDataAccess;
using SmartAppCore.Interfaces.Persistence;
using SmartAppDataAccess.DBConnectionSettings;
using SmartAppRepository.Registration;
using SmartAppDataAccess.Registration;
using SmartAppCore.Registration;
//using SmartAppDataAccess.Mocks;

namespace SmartAppService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartAppService", Version = "v1" });
            });
            //************ Customized DI ****************
            //services.AddScoped<ISearchValidator, SearchValidator>();
            //services.AddScoped<ISearchRepository, SearchRepository>();
            //services.AddScoped<INoSQLSearchDataAccess, NoSQLSearchDataAccess>();
            services.AddCoreServices();
            services.AddRepositoryServices();
            services.AddDataAccessServices(Configuration);
            services.AddSingleton(Configuration.GetSection("ElasticSearchCnxSettings").Get<ElasticSearchCnxSettings>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartAppService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
