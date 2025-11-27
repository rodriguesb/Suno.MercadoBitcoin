using Suno.MercadoBitcoin.Application.Dtos;
using Suno.MercadoBitcoin.Application.Interfaces;
using Suno.MercadoBitcoin.Infra.External.HttpClients.Interfaces;

namespace Suno.MercadoBitcoin.Application.Services;

public class AccountPositionService : IAccountPositionService
{
    private readonly IMercadoBitcoinClient _client;

    public AccountPositionService(IMercadoBitcoinClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<PositionDto>> GetPositionsAsync(string accountId, string token)
    {
        var result = await _client.GetPositionsAsync(accountId, token);

        return result.Select(x => new PositionDto
        {
            AvgPrice = x.AvgPrice,
            Category = x.Category,
            Id = x.Id,
            Instrument = x.Instrument,
            Qty = decimal.TryParse(x.Qty, out var q) ? q : 0,
            Side = x.Side
        });
    }
}
