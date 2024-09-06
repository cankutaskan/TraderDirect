using Microsoft.Extensions.DependencyInjection;
using TraderDirect.Domain.Ports.IServices;
using TraderDirect.Domain.Services;

namespace TraderDirect.Domain.Config;
public static class DomainConfigHelper
{
    public static void DomainConfig(this IServiceCollection services)
    {
        services.AddScoped<IGetTradesUserService, GetTradesUserService>();
    }
}

