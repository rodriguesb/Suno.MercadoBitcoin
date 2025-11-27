using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Suno.MercadoBitcoin.Application.DependencyInjection;
using Suno.MercadoBitcoin.Application.Interfaces;
using Suno.MercadoBitcoin.Infra.External.DependencyInjections;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Suno.MercadoBitcoin.IntegrationTest;

public class ErrorAuthorizationTests : IDisposable
{
    private readonly WireMockServer _server;

    public ErrorAuthorizationTests()
    {
        _server = WireMockServer.Start();
    }

    [Theory]
    [InlineData(401)]
    [InlineData(403)]
    public async Task ShouldThrowExceptionWhenUnauthorized(int statusCode)
    {
        _server
            .Given(Request.Create()
                .WithPath("/accounts/999/positions")
                .UsingGet()
            )
            .RespondWith(Response.Create().WithStatusCode(statusCode));

        var services = new ServiceCollection();
        services.AddApplicationServices();
        services.AddMercadoBitcoin(_server.Url);

        var provider = services.BuildServiceProvider();
        var service = provider.GetRequiredService<IAccountPositionService>();

        Func<Task> act = async () =>
            await service.GetPositionsAsync("999", "invalid-token");

        await act.Should().ThrowAsync<Exception>();
    }

    public void Dispose() => _server.Dispose();
}
