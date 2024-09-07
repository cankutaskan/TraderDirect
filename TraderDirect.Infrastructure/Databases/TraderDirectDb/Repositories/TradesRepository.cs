using Microsoft.EntityFrameworkCore;
using TraderDirect.Domain.Ports.Contracts;
using TraderDirect.Domain.Ports.Repositories;
using TraderDirect.Infrastructure.Databases.TraderDirectDb;

namespace TraderDirect.Infrastructure.Databases.TraderDirectDb.Repositories;
public class TradesRepository(TraderDirectDbContext dbContext) : ITradesRepository
{
    private readonly TraderDirectDbContext _dbContext = dbContext;

    public async Task<IEnumerable<ITrade>> GetUserTrades(int userId, CancellationToken cancellationToken) =>
        await _dbContext.Trades.Where(t => t.UserId == userId).ToListAsync(cancellationToken);
}

