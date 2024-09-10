using Microsoft.EntityFrameworkCore;
using TraderDirect.Infrastructure.Databases.TraderDirectDb;

namespace TraderDirect.Test.UnitTests;
public static class DbContextFactory
{
    public static TraderDirectDbContext CreateInMemoryDbContext()
    {
        DbContextOptions<TraderDirectDbContext> options = new DbContextOptionsBuilder<TraderDirectDbContext>()
            .UseSqlite("Data Source=:memory:")
            .Options;

        TraderDirectDbContext dbContext = new TraderDirectDbContext(options);

        dbContext.Database.OpenConnection();
        dbContext.Database.EnsureCreated();

        return dbContext;
    }
}

