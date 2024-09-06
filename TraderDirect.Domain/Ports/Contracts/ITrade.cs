namespace TraderDirect.Domain.Ports.Contracts;
public interface ITrade
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Asset { get; set; }
    public double Quantity { get; set; }
    public double Price { get; set; }
    public DateTime CreateDate { get; set; }
}

