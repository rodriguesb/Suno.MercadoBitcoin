using Suno.MercadoBitcoin.Application.Dtos;
using Suno.MercadoBitcoin.Application.Queries;

namespace Suno.MercadoBitcoin.Application.Interfaces;

public interface IAccountPositionService
{
    Task<IEnumerable<PositionDto>> GetPositionsAsync(string accountId, string token);
}
