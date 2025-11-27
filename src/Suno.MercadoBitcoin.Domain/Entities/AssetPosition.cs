namespace Suno.MercadoBitcoin.Domain.Entities;

public class AssetPosition
{
    public string Symbol { get; }
    public decimal Quantity { get; }
    public decimal CurrentPrice { get; }
    public DateTime UpdatedAt { get; }

    public AssetPosition(string symbol, decimal quantity, decimal currentPrice, DateTime updatedAt)
    {
        Symbol = symbol;
        Quantity = quantity;
        CurrentPrice = currentPrice;
        UpdatedAt = updatedAt;
    }
}
