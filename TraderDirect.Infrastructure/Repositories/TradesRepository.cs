using Microsoft.EntityFrameworkCore;
using TraderDirect.Domain.Ports.Contracts;
using TraderDirect.Domain.Ports.Repositories;
using TraderDirect.Infrastructure.Databases.TraderDirectDb;

namespace TraderDirect.Infrastructure.Repositories;
public class TradesRepository : ITradesRepository
{
    private readonly TraderDirectDbContext _dbContext;

    public TradesRepository(TraderDirectDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ITrade>> GetUserTrades(int userId) =>
        await _dbContext.Trades.Where(t => t.UserId == userId).ToListAsync();
}

