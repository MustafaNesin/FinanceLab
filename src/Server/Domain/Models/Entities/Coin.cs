namespace FinanceLab.Server.Domain.Models.Entities;

public class Coin
{
    public string Symbol { get; set; }
    public double Price { get; set; }

    public override string ToString() => string.Join(" ", Symbol, Price);
}