using WilmerFlorez.Logic.Implementations;
using WilmerFlorez.Logic.Interfaces;
using WilmerFlorez.Persistence.Implementations;
using WilmerFlorez.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WilmerFlorez.Api.Extensions
{
    public static class Application
    {
        public static void AddServices(this IServiceCollection services, Type dbContextType)
        {
            services.AddScoped<IImageLogic, ImageLogic>();
            services.AddScoped<IOwnerLogic, OwnerLogic>();
            services.AddScoped<IPropertyImageLogic, PropertyImageLogic>();
            services.AddScoped<IPropertyLogic, PropertyLogic>();
            services.AddScoped<IContext, Context>();
            

            services.AddScoped<IOwnerPersistence, OwnerPersistence>();
            services.AddScoped<IPropertyImagePersistence, PropertyImagePersistence>();
            services.AddScoped<IPropertyPersistence, PropertyPersistence>();

            services.AddScoped(typeof(DbContext), dbContextType);
            

        }

        public static void UseMq<TContext>(this IServiceCollection services, IConfiguration configuration) where TContext : DbContext
        {
            var rabbitMqHost = Environment.GetEnvironmentVariable("RABBITMQ");
            services.AddCap(config =>
            {
                config.UseEntityFramework<TContext>();
                config.FailedRetryInterval = 120;
                config.UseRabbitMQ(rabbitMq => {
                    rabbitMq.HostName = rabbitMqHost;
                    rabbitMq.UserName = configuration["rabbitmq:UserName"];
                    rabbitMq.Password = configuration["rabbitmq:Password"];
                    rabbitMq.ExchangeName = "WilmerFlorez.router";
                });
            });
        }
    }
}
