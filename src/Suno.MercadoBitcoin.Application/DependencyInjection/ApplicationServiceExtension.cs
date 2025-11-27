using Microsoft.Extensions.DependencyInjection;
using Suno.MercadoBitcoin.Application.Interfaces;
using Suno.MercadoBitcoin.Application.Services;

namespace Suno.MercadoBitcoin.Application.DependencyInjection;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountPositionService, AccountPositionService>();

        return services;
    }
}
