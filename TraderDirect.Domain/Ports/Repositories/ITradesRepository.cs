using System;
using TraderDirect.Domain.Ports.Contracts;

namespace TraderDirect.Domain.Ports.Repositories;
public interface ITradesRepository
{
    Task<IEnumerable<ITrade>> GetUserTrades(int userId);
}

