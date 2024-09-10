using System;
using TraderDirect.Domain.Models;
using TraderDirect.Domain.Ports.Contracts;

namespace TraderDirect.Domain.Ports.Repositories;
public interface ITradesRepository
{
    Task<IEnumerable<ITrade>> GetUserTrades(int userId, CancellationToken cancellationToken);

    Task CreateTrades(List<ExecuteTradeServiceRequest> trades, CancellationToken cancellationToken);

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

