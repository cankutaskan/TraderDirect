using System.ComponentModel.DataAnnotations;
using TraderDirect.App.ApiModels.Requests.Validation;

namespace TraderDirect.App.ApiModels.Requests;

public record ExecuteTradesRequest()
{
    public List<CreateTradeRequest> Trades { get; set; } = new();

    [Range(1, int.MaxValue, ErrorMessage = "UserId must have a value greater than 0.")]
    public int UserId { get; set; }
    
}
public record CreateTradeRequest
{
    [Required(ErrorMessage = "Asset is required.")]
    public required string Asset { get; set; }

    [NotEqualToZero(ErrorMessage = "Quantity cannot be zero. Use positive for buy and negative for sell.")]
    public double Quantity { get; set; }

    [GreaterThanZero(ErrorMessage = "Price must be greater than zero.")]
    public double Price { get; set; }
}

