using System;
using TraderDirect.Domain.Models;
using TraderDirect.Domain.Ports.Contracts;

namespace TraderDirect.Domain.Ports.Repositories;
public interface ITradesRepository
{
    Task<IEnumerable<ITrade>> GetUserTrades(int userId, CancellationToken cancellationToken);

    Task CreateTrades(List<ExecuteTradeServiceRequest> trades, CancellationToken cancellationToken);

    Task<IEnumerable<ITrade>> GetAllTrades(CancellationToken cancellationToken);
}

