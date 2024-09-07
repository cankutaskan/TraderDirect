using TraderDirect.Domain.Ports.Contracts;

namespace TraderDirect.App.ApiModels.Responses;

public record TradesUserResponse()
{
    public Dictionary<int, List<Trade>> Trades { get; set; } = new();

    public TradesUserResponse(List<ITrade> trades) : this()
    {
        Trades = trades
            .GroupBy(t => t.UserId)
            .ToDictionary(
                g => g.Key,
                g => g.Select(t => new Trade
                {
                    Id = t.Id,
                    Asset = t.Asset,
                    Quantity = t.Quantity,
                    Price = t.Price,
                    CreateDate = t.CreateDate
                }).ToList());
    }
}

public record Trade
{
    public int Id { get; set; }
    public required string Asset { get; set; }
    public double Quantity { get; set; }
    public double Price { get; set; }
    public DateTime CreateDate { get; set; }
}

