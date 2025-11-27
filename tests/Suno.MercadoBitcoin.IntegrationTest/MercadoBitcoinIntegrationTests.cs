using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Suno.MercadoBitcoin.Infra.External.DependencyInjections;
using Suno.MercadoBitcoin.Application.Interfaces;
using WireMock.Server;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;
using Suno.MercadoBitcoin.Application.DependencyInjection;

namespace Suno.MercadoBitcoin.IntegrationTest;

public class MercadoBitcoinIntegrationTests : IDisposable
{
    private readonly WireMockServer _server;

    public MercadoBitcoinIntegrationTests()
    {
        _server = WireMockServer.Start();

        _server
            .Given(
                Request.Create()
                    .WithPath("/accounts/123/positions")
                    .UsingGet()
                    .WithHeader("Authorization", "Bearer my-secret-token")
            )
            .RespondWith(
                Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                    new {
                        avgPrice = 350000,
                        category = "limit",
                        id = "1",
                        instrument = "BTC-BRL",
                        qty = "0.5",
                        side = "buy"
                    }
                    })
            );
    }

    [Fact]
    public async Task ShouldCallApiWithBearerTokenAndReturnMappedData()
    {
        var services = new ServiceCollection();

        services.AddApplicationServices();
        services.AddMercadoBitcoin(_server.Url);
        var provider = services.BuildServiceProvider();

        var service = provider.GetRequiredService<IAccountPositionService>();

        var result = await service.GetPositionsAsync("123", "my-secret-token");

        result.Should().HaveCount(1);
        result.First().Instrument.Should().Be("BTC-BRL");
        result.First().Qty.Should().Be(0.5m);
    }

    public void Dispose()
    {
        _server.Stop();
        _server.Dispose();
    }
}
