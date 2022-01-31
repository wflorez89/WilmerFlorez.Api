using FluentValidation.AspNetCore;
using WilmerFlorez.Database;
using WilmerFlorez.Models.Validation;
using WilmerFlorez.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace WilmerFlorez.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());
            builder.AddEnvironmentVariables();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ContextDb>(
                options =>
                    options.UseSqlServer(connectionString),
                ServiceLifetime.Scoped);

            services.AddControllers().AddFluentValidation(fv => fv
                    .RegisterValidatorsFromAssemblyContaining<FilterInputValidation>()
                );
            services.AddServices(typeof(ContextDb));

            

            //Swagger
            AddSwagger(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ContextDb dataContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "WilmerFlorez");
            });


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            UpdateDatabase(app);
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ContextDb>();
                var iscreated = context.Database.EnsureCreated();
            }
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"WilmerFlorez {groupName}",
                    Version = groupName,
                    Description = "Wilmer Florez API",
                    Contact = new OpenApiContact
                    {
                        Name = "WilmerFlorez",
                        Email = string.Empty,
                        Url = new Uri("https://rostec.com/"),
                    }
                });
            });
        }
    }
}
