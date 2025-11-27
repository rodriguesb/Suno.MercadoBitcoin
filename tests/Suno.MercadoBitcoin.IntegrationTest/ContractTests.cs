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

public class ContractTests : IDisposable
{
    private readonly WireMockServer _server;

    public ContractTests()
    {
        _server = WireMockServer.Start();
    }

    [Theory]
    [InlineData("BTC-BRL", "0.5")]
    [InlineData("ETH-BRL", "2")]
    [InlineData("LTC-BRL", "10")]
    public async Task ShouldHonorApiContract(string instrument, string qty)
    {
        _server.Given(Request.Create().WithPath("/accounts/test/positions").UsingGet())
            .RespondWith(Response.Create().WithBodyAsJson(new[] {
                new {
                    avgPrice = 120000,
                    category = "limit",
                    id = Guid.NewGuid().ToString(),
                    instrument,
                    qty,
                    side = "buy"
                }
            }));

        var services = new ServiceCollection();
        services.AddApplicationServices();
        services.AddMercadoBitcoin(_server.Url);

        var provider = services.BuildServiceProvider();
        var service = provider.GetRequiredService<IAccountPositionService>();

        var result = await service.GetPositionsAsync("test", "token");

        result.Single().Instrument.Should().Be(instrument);
        result.Single().Qty.Should().Be(decimal.Parse(qty));
    }

    public void Dispose() => _server.Dispose();
}
