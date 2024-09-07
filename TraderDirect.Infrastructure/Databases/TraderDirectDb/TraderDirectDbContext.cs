using Microsoft.EntityFrameworkCore;
using TraderDirect.Domain.Ports.Contracts;
using TraderDirect.Infrastructure.Databases.TraderDirectDb.Entities;

namespace TraderDirect.Infrastructure.Databases.TraderDirectDb;
public class TraderDirectDbContext(DbContextOptions<TraderDirectDbContext> options) : DbContext(options)
{
    public DbSet<TradeEntity> Trades { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TradeEntity>()
        .HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(t => t.UserId);

        base.OnModelCreating(modelBuilder);
    }
}

