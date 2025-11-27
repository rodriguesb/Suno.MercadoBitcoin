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

public class RetryPolicyTests : IDisposable
{
    private readonly WireMockServer _server;

    public RetryPolicyTests()
    {
        _server = WireMockServer.Start();

        const string scenario = "retry-flow";

        // Primeira chamada -> 500
        _server
            .Given(Request.Create()
                .WithPath("/accounts/retry/positions")
                .UsingGet())
            .InScenario(scenario)
            .WillSetStateTo("second")
            .RespondWith(Response.Create()
                .WithStatusCode(500));

        // Segunda chamada -> 500
        _server
            .Given(Request.Create()
                .WithPath("/accounts/retry/positions")
                .UsingGet())
            .InScenario(scenario)
            .WhenStateIs("second")
            .WillSetStateTo("success")
            .RespondWith(Response.Create()
                .WithStatusCode(500));

        // Terceira chamada -> sucesso
        _server
            .Given(Request.Create()
                .WithPath("/accounts/retry/positions")
                .UsingGet())
            .InScenario(scenario)
            .WhenStateIs("success")
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody("""
                [
                  {
                    "avgPrice": 100,
                    "qty": "1",
                    "category": "limit",
                    "id": "X",
                    "instrument": "ETH-BRL",
                    "side": "buy"
                  }
                ]
                """));
    }

    [Fact]
    public async Task ShouldRetryAndEventuallySucceed()
    {
        var services = new ServiceCollection();
        services.AddApplicationServices();
        services.AddMercadoBitcoin(_server.Url, TimeSpan.FromSeconds(15));

        var provider = services.BuildServiceProvider();
        var service = provider.GetRequiredService<IAccountPositionService>();

        var result = await service.GetPositionsAsync("retry", "token");

        result.Should().HaveCount(1);

        _server.LogEntries.Count.Should().BeGreaterThan(2);
    }

    public void Dispose()
    {
        _server.Stop();
        _server.Dispose();
    }
}
