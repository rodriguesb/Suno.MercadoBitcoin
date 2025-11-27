using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Suno.MercadoBitcoin.Application.DependencyInjection;
using Suno.MercadoBitcoin.Application.Interfaces;
using Suno.MercadoBitcoin.Infra.External.DependencyInjections;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace Suno.MercadoBitcoin.IntegrationTest;

public class TimeoutTests : IDisposable
{
    private readonly WireMockServer _server;

    public TimeoutTests()
    {
        _server = WireMockServer.Start();

        _server
            .Given(Request.Create().WithPath("/accounts/slow/positions").UsingGet())
            .RespondWith(Response.Create()
                .WithDelay(TimeSpan.FromSeconds(10)) // maior que timeout
                .WithBody("timeout"));
    }

    [Fact]
    public async Task ShouldFailOnTimeout()
    {
        var services = new ServiceCollection();
        services.AddApplicationServices();
        services.AddMercadoBitcoin(_server.Url);
        var provider = services.BuildServiceProvider();

        var service = provider.GetRequiredService<IAccountPositionService>();

        Func<Task> act = async () =>
            await service.GetPositionsAsync("slow", "token");

        await act.Should().ThrowAsync<TaskCanceledException>();
    }

    public void Dispose() => _server.Dispose();
}
