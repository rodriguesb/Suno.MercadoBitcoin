using Moq;
using Suno.MercadoBitcoin.Infra.External.HttpClients.Interfaces;
using Suno.MercadoBitcoin.Infra.External.HttpClients.Models;

namespace Suno.MercadoBitcoin.UnitTest.Mocks;

public static class MercadoBitcoinClientMock
{
    public static Mock<IMercadoBitcoinClient> Create()
    {
        var mock = new Mock<IMercadoBitcoinClient>();

        mock
            .Setup(x => x.GetPositionsAsync("123", "my-secret-token"))
            .ReturnsAsync(new List<PositionModel>
            {
            new() {
                Id = "1",
                Instrument = "BTC-BRL",
                Qty = "0.5",
                AvgPrice = 350000,
                Category = "limit",
                Side = "buy"
            }
            });

        return mock;
    }
}
