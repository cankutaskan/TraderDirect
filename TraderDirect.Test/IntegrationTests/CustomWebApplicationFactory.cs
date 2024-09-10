using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using TraderDirect.Infrastructure.Databases.TraderDirectDb;
using TraderDirect.Infrastructure.Providers;

namespace TraderDirect.Test.IntegrationTests;
public class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
{
    public SqliteConnection DbConnection;
    protected HttpClient? _client;
    private readonly string _connectionString = "DataSource=:memory:";
    private readonly Mock<IRabbitMqProvider> _rabbitMqMock;

    public CustomWebApplicationFactory()
    {
        DbConnection = new SqliteConnection(_connectionString);
        DbConnection.Open();
        _rabbitMqMock = new Mock<IRabbitMqProvider>();
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
            services.AddSingleton<IRabbitMqProvider>(_rabbitMqMock.Object);
        });
    }
}

