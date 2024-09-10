using TraderDirect.Domain.Ports.Contracts;

namespace TraderDirect.Domain.Ports.Repositories;
public interface ITradesRepository
{
    /// <summary>
    /// Get user trades.
    /// </summary>
    /// <param name="userId">ID of the user</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>List of trades</returns>
    Task<IEnumerable<ITrade>> GetUserTrades(int userId, CancellationToken cancellationToken);

    /// <summary>
    /// Create trades in database.
    /// </summary>
    /// <param name="trades">List of trades to be insered</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Task</returns>
    Task CreateTrades(List<ExecuteTradeServiceRequest> trades, CancellationToken cancellationToken);

    /// <summary>
    /// Get all trades.
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>List of trades</returns>
    Task<IEnumerable<ITrade>> GetAllTrades(CancellationToken cancellationToken);

    public record ExecuteTradesServiceRequest(List<ExecuteTradeServiceRequest> Trades);
    public record ExecuteTradeServiceRequest
    {
        public int UserId { get; set; }
        public required string Asset { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
    }
}

