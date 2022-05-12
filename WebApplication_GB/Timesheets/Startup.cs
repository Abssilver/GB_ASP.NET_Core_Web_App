using Authentication;
using Authentication.BusinessLayer;
using Authentication.DataLayer;
using Authentication.Migrations;
using BusinessLogic;
using DataLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Migrations;
using Timesheets.Extensions;

namespace Timesheets
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
            services.RegisterAuthentication(Configuration);
            services.RegisterAuthenticationBusinessLayer();
            services.RegisterAuthenticationDataLayer();
            services.RegisterAuthenticationMigrations(Configuration);
            services.RegisterMigrations(Configuration);
            services.AddControllers();
            services.ConfigureBackendSwagger();
            services.Configure<ServiceProperties>(Configuration.GetSection(nameof(ServiceProperties)));
            services.RegisterBusinessLogic();
            services.RegisterDataLayer();
            services.RegisterValidationServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Timesheets v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors("AuthPolicy");
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}