using TraderDirect.Domain.Ports.Contracts;

namespace TraderDirect.Infrastructure.Databases.TraderDirectDb.Entities;
public class TradeEntity : ITrade
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public required string Asset { get; set; }
    public double Quantity { get; set; }
    public double Price { get; set; }
    public DateTime CreateDate { get; set; }
}

