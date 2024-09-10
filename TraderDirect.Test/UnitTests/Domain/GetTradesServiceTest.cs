using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderDirect.Domain.Ports.Contracts;
using TraderDirect.Domain.Ports.Repositories;
using TraderDirect.Domain.Services;
using TraderDirect.Infrastructure.Databases.TraderDirectDb.Entities;

namespace TraderDirect.Test.UnitTests.Domain;
public class GetTradesServiceTest
{
    private readonly Mock<ITradesRepository> _mockTradesRepository;
    private readonly GetTradesService _getTradesService;

    public GetTradesServiceTest()
    {
        _mockTradesRepository = new Mock<ITradesRepository>();
        _getTradesService = new GetTradesService(_mockTradesRepository.Object);
    }

    [Fact]
    public async Task HandleAsync_WithUserId_ShouldReturnUserTrades()
    {
        // Arrange
        int userId = 1;
        CancellationToken cancellationToken = new();
        List<ITrade> expectedTrades = new()
        {
             new TradeEntity() { Asset = "1", Price = 1, Quantity = 1, CreateDate = DateTime.UtcNow, UserId = userId },
             new TradeEntity() { Asset = "2", Price = 2, Quantity = 2, CreateDate = DateTime.UtcNow, UserId = userId }
        };

        _mockTradesRepository
            .Setup(repo => repo.GetUserTrades(userId, cancellationToken))
            .ReturnsAsync(expectedTrades);

        // Act
        List<ITrade> result = await _getTradesService.HandleAsync(userId, cancellationToken);

        // Assert
        Assert.Equal(expectedTrades, result);
        _mockTradesRepository.Verify(repo => repo.GetUserTrades(userId, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnAllTrades()
    {
        // Arrange
        CancellationToken cancellationToken = new();
        List<ITrade> expectedTrades = new()
        {
             new TradeEntity() { Asset = "1", Price = 1, Quantity = 1, CreateDate = DateTime.UtcNow, UserId = 1 },
             new TradeEntity() { Asset = "2", Price = 2, Quantity = 2, CreateDate = DateTime.UtcNow, UserId = 2 }
        };

        _mockTradesRepository
            .Setup(repo => repo.GetAllTrades(cancellationToken))
            .ReturnsAsync(expectedTrades);

        // Act
        List<ITrade> result = await _getTradesService.HandleAsync(cancellationToken);

        // Assert
        Assert.Equal(expectedTrades, result); 
        _mockTradesRepository.Verify(repo => repo.GetAllTrades(cancellationToken), Times.Once); 
    }
}


