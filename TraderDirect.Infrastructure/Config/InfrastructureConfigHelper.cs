using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderDirect.Infrastructure.Databases.TraderDirectDb;

namespace TraderDirect.Infrastructure.Config;
public static class InfrastructureConfigHelper
{
    public static void InfrastructureConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDbConnectipn(configuration);
        services.ConfigureServices();
    }

    public static void ConfigureDbConnectipn(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TraderDirectDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("TraderDirect.Infrastructure")));
    }

    public static void ConfigureServices(this IServiceCollection services)
    {

    }
}
