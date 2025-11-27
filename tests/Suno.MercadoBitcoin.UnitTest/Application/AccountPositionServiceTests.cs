using FluentAssertions;
using Suno.MercadoBitcoin.Application.Services;
using Suno.MercadoBitcoin.UnitTest.Mocks;

namespace Suno.MercadoBitcoin.UnitTest.Application;

public class AccountPositionServiceTests
{
    [Fact]
    public async Task ShouldMapResponseCorrectly()
    {
        var mockClient = MercadoBitcoinClientMock.Create();
        var service = new AccountPositionService(mockClient.Object);

        var result = await service.GetPositionsAsync("123", "my-secret-token");

        result.Should().HaveCount(1);

        var position = result.First();
        position.Instrument.Should().Be("BTC-BRL");
        position.Qty.Should().Be(0.5m);
        position.AvgPrice.Should().Be(350000);
    }
}
