using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text.Json;
using TraderDirect.Domain.Ports.Contracts;
using TraderDirect.Domain.Ports.Repositories;
using TraderDirect.Infrastructure.Databases.TraderDirectDb.Entities;
using TraderDirect.Infrastructure.Providers;
using static TraderDirect.Domain.Ports.Repositories.ITradesRepository;

namespace TraderDirect.Infrastructure.Databases.TraderDirectDb.Repositories;
public class TradesRepository(TraderDirectDbContext dbContext, IRabbitMqProvider rabbitMqProvider) : ITradesRepository
{
    private readonly TraderDirectDbContext _dbContext = dbContext;
    private readonly IRabbitMqProvider _rabbitMqProvider = rabbitMqProvider;

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

        IExecutionStrategy strategy = _dbContext.Database.CreateExecutionStrategy();
        await strategy.Execute(
              async () =>
              {
                  using IDbContextTransaction transaction = _dbContext.Database.BeginTransaction();
                  await _dbContext.Trades.AddRangeAsync(entities, cancellationToken);
                  await _dbContext.SaveChangesAsync(cancellationToken);
                  string jsonString = JsonSerializer.Serialize(entities);
                  _rabbitMqProvider.Publish(jsonString);
                  await transaction.CommitAsync(cancellationToken);
              });
    }

    public async Task<IEnumerable<ITrade>> GetAllTrades(CancellationToken cancellationToken)
    {
        IEnumerable<ITrade> result = await _dbContext.Trades.ToListAsync(cancellationToken);
        return result;
    }
}

