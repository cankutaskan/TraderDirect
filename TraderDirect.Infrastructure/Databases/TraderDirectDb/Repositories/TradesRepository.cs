using Microsoft.EntityFrameworkCore;
using TraderDirect.Domain.Models;
using TraderDirect.Domain.Ports.Contracts;
using TraderDirect.Domain.Ports.Repositories;
using TraderDirect.Infrastructure.Databases.TraderDirectDb;
using TraderDirect.Infrastructure.Databases.TraderDirectDb.Entities;

namespace TraderDirect.Infrastructure.Databases.TraderDirectDb.Repositories;
public class TradesRepository(TraderDirectDbContext dbContext) : ITradesRepository
{
    private readonly TraderDirectDbContext _dbContext = dbContext;

    public async Task<IEnumerable<ITrade>> GetUserTrades(int userId, CancellationToken cancellationToken) =>
        await _dbContext.Trades.Where(t => t.UserId == userId).ToListAsync(cancellationToken);

    public async Task CreateTrades(List<ExecuteTradeServiceRequest> trades, CancellationToken cancellationToken)
    {
        List<TradeEntity> entities = trades
               .Select(trade => new TradeEntity
               {
                   UserId = trade.UserId,
                   Asset = trade.Asset,
                   Quantity = trade.Quantity,
                   Price = trade.Price,
                   CreateDate = DateTime.UtcNow
               }).ToList();

        await _dbContext.Trades.AddRangeAsync(entities, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

