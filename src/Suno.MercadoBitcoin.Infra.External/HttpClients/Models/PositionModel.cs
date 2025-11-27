namespace Suno.MercadoBitcoin.Infra.External.HttpClients.Models;

public class PositionModel
{
    public decimal AvgPrice { get; set; }
    public string Category { get; set; }
    public string Id { get; set; }
    public string Instrument { get; set; }
    public string Qty { get; set; }
    public string Side { get; set; }
}
