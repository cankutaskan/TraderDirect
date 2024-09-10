using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TraderDirect.Infrastructure.Databases.TraderDirectDb;

namespace TraderDirect.Test.IntegrationTests;
public class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
{
    public SqliteConnection DbConnection;
    private readonly string _connectionString = "DataSource=:memory:";

    public CustomWebApplicationFactory()
    {
        DbConnection = new SqliteConnection(_connectionString);
        DbConnection.Open();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<TraderDirectDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.RemoveAll(typeof(DbContextOptions<TraderDirectDbContext>));
            services.AddEntityFrameworkSqlite().AddDbContext<TraderDirectDbContext>(options =>
            {
                options.UseSqlite(DbConnection);
            });

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TraderDirectDbContext>();
            dbContext.Database.OpenConnection();
            dbContext.Database.EnsureCreated();
        });
    }
}

