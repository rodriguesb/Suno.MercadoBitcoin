using Microsoft.Extensions.DependencyInjection;
using Refit;
using Suno.MercadoBitcoin.Infra.External.HttpClients.Interfaces;
using Suno.MercadoBitcoin.Infra.External.Policies;

namespace Suno.MercadoBitcoin.Infra.External.DependencyInjections;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMercadoBitcoin(
    this IServiceCollection services,
    string baseUrl,
    TimeSpan? timeout = null)
    {
        services
            .AddRefitClient<IMercadoBitcoinClient>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(baseUrl);
                c.Timeout = timeout ?? TimeSpan.FromSeconds(10);
            })
            .AddPolicyHandler(ResiliencePolicies.GetRetryPolicy());

        return services;
    }
}
