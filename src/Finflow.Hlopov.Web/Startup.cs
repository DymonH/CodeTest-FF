using Finflow.Hlopov.Application.Interfaces;
using Finflow.Hlopov.Application.Services;
using Finflow.Hlopov.Core.Configuration;
using Finflow.Hlopov.Core.Interfaces;
using Finflow.Hlopov.Core.Repositories;
using Finflow.Hlopov.Core.Repositories.Base;
using Finflow.Hlopov.Infrastructure.Data;
using Finflow.Hlopov.Infrastructure.Logging;
using Finflow.Hlopov.Infrastructure.Repository;
using Finflow.Hlopov.Infrastructure.Repository.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Finflow.Hlopov.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Core Layer
            services.Configure<FinflowSettings>(Configuration);

            // Add Infrastructure Layer
            ConfigureDatabases(services);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IRemmittanceRepository, RemittanceRepository>();
            services.AddScoped(typeof(IApplicationLogger<>), typeof(LoggerAdapter<>));

            // Add Application Layer
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IRemmittanceService, RemmittanceService>();

            // Add Web Layer
            services.AddControllers();

            // Add Miscellaneous
            services.AddHttpContextAccessor();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Finflow.Hlopov WebAPI",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Noname",
                        Email = string.Empty,
                        Url = new Uri("https://example.com/noname"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureDatabases(IServiceCollection services)
        {
            // use in-memory database
            //services.AddDbContext<FinflowContext>(c =>
            //    c.UseInMemoryDatabase("FinflowConnection"));

            // use real database
            services.AddDbContext<FinflowContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("FinFlowConnection")));
        }
    }
}