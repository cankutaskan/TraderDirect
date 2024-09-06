using Microsoft.EntityFrameworkCore;
using TraderDirect.Infrastructure.Databases.TraderDirectDb.Entities;

namespace TraderDirect.Infrastructure.Databases.TraderDirectDb;
public class TraderDirectDbContext : DbContext
{
    public DbSet<TradeEntity> Trades { get; set; }
    public DbSet<UserEntity> Users { get; set; }
}

