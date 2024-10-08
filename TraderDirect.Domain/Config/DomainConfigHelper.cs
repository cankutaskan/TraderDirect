﻿using Microsoft.Extensions.DependencyInjection;
using TraderDirect.Domain.Ports.Services;
using TraderDirect.Domain.Services;

namespace TraderDirect.Domain.Config;
public static class DomainConfigHelper
{
    public static void DomainConfig(this IServiceCollection services)
    {
        services.AddScoped<IGetTradesService, GetTradesService>();
        services.AddScoped<ICreateUserService, CreateUserService>();
        services.AddScoped<IExecuteTradesService, ExecuteTradesService>();
    }
}

