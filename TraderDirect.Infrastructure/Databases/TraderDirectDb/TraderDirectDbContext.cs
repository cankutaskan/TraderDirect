using Microsoft.EntityFrameworkCore;

namespace TraderDirect.Infrastructure.Databases.TraderDirectDb;
public class TraderDirectDbContext : DbContext
{
    public DbSet<Trade> Trades { get; set; }
    public DbSet<User> Trades { get; set; }
    public TraderDirectDbContext(DbContextOptions<TraderDirectDbContext> options) : base(options)
    {

    }
}

