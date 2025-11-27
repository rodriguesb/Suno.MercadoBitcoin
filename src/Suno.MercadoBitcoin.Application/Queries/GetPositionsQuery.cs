namespace Suno.MercadoBitcoin.Application.Queries;

public record GetPositionsQuery(DateTime? StartDate, DateTime? EndDate);
