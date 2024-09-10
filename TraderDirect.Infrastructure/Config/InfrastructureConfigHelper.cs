using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using TraderDirect.Domain.Ports.Repositories;
using TraderDirect.Infrastructure.Databases.TraderDirectDb;
using TraderDirect.Infrastructure.Databases.TraderDirectDb.Repositories;
using TraderDirect.Infrastructure.Providers;

namespace TraderDirect.Infrastructure.Config;
public static class InfrastructureConfigHelper
{
    public static void InfrastructureConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDbConnectipn(configuration);
        services.ConfigureServices();
        services.ConfigureRabbitMq(configuration);
    }

    public static void ConfigureDbConnectipn(this IServiceCollection services, IConfiguration configuration)
    {
        string? cs = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<TraderDirectDbContext>(options =>
            options.UseSqlServer(cs));
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<ITradesRepository, TradesRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    public static void ConfigureRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        IConfigurationSection rabbitMqConfig = configuration.GetSection("RabbitMq");
        services.Configure<RabbitMqConfig>(rabbitMqConfig);
        services.AddSingleton<IRabbitMqProvider, RabbitMqProvider>();
    }
}
