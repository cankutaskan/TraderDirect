using Microsoft.EntityFrameworkCore;
using Moq;
using static TraderDirect.Domain.Ports.Repositories.ITradesRepository;
using TraderDirect.Infrastructure.Databases.TraderDirectDb.Repositories;
using TraderDirect.Infrastructure.Providers;
using TraderDirect.Infrastructure.Databases.TraderDirectDb;
using TraderDirect.Infrastructure.Databases.TraderDirectDb.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TraderDirect.Test.Domain;
public class TradesRepositoryTest
{
    private readonly TraderDirectDbContext _dbContext;
    private readonly Mock<IRabbitMqProvider> _mockRabbitMqProvider;
    private readonly TradesRepository _tradesRepository;

    public TradesRepositoryTest()
    {
        _dbContext = DbContextFactory.CreateInMemoryDbContext();
        _mockRabbitMqProvider = new Mock<IRabbitMqProvider>();
        _tradesRepository = new TradesRepository(_dbContext, _mockRabbitMqProvider.Object);
    }

    [Fact]
    public async Task CreateTrades_TradesCreated_RabbitMqProviderIsCalled()
    {
        // Arrange
        EntityEntry<UserEntity> newUser = _dbContext.Users.Add(new UserEntity() { Email = "a@a.com" });
        _dbContext.SaveChanges();
        List<ExecuteTradeServiceRequest> tradeRequests = new()
        {
            new ExecuteTradeServiceRequest(){ Asset = "1", Price = 2, Quantity = -10, UserId = newUser.Entity.Id }
        };
        CancellationToken cancellationToken = new();


        // Act
        await _tradesRepository.CreateTrades(tradeRequests, cancellationToken);

        // Assert
        TradeEntity record = _dbContext.Trades.First();
        Assert.NotNull(record);
        Assert.Equal("1", record.Asset);
        Assert.Equal(2, record.Price);
        Assert.Equal(-10, record.Quantity);
        Assert.Equal(newUser.Entity.Id, record.UserId);

        _mockRabbitMqProvider.Verify(mq => mq.Publish(It.IsAny<string>()), Times.Once);
    }
}

