using Refit;
using Suno.MercadoBitcoin.Infra.External.HttpClients.Models;

namespace Suno.MercadoBitcoin.Infra.External.HttpClients.Interfaces;

public interface IMercadoBitcoinClient
{
    [Get("/accounts/{accountId}/positions")]
    Task<List<PositionModel>> GetPositionsAsync(
        string accountId,
        [Authorize("Bearer")] string token
    );
}
