namespace Suno.MercadoBitcoin.Application.Dtos;

public class PositionDto
{
    public decimal AvgPrice { get; set; }
    public string Category { get; set; }
    public string Id { get; set; }
    public string Instrument { get; set; }
    public decimal Qty { get; set; }
    public string Side { get; set; }
}
