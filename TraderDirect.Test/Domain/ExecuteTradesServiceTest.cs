using Moq;
using TraderDirect.Domain.Ports.Repositories;
using TraderDirect.Domain.Services;
using TraderDirect.Infrastructure.Providers;
using static TraderDirect.Domain.Ports.Repositories.ITradesRepository;

namespace TraderDirect.Test.UnitTests
{
    public class ExecuteTradesServiceTest
    {
        private readonly Mock<ITradesRepository> _mockTradesRepository;
        private readonly ExecuteTradesService _executeTradesService;

        public ExecuteTradesServiceTest()
        {
            _mockTradesRepository = new Mock<ITradesRepository>();
            _executeTradesService = new ExecuteTradesService(_mockTradesRepository.Object);
        }

        [Fact]
        public async Task ExecuteTrade_ExecutesTrades_RepositoryIsCalled()
        {
            // Arrange
            List<ExecuteTradeServiceRequest> tradeRequest = new()
            {
                new ExecuteTradeServiceRequest(){ Asset= "1", Price = 1, Quantity = 1, UserId = 1 }
            };

            CancellationToken cancellationToken = new();
            _mockTradesRepository
                .Setup(repo => repo.CreateTrades(It.IsAny<List<ExecuteTradeServiceRequest>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _executeTradesService.HandleAsync(tradeRequest, cancellationToken);

            // Assert
            _mockTradesRepository.Verify(repo => repo.CreateTrades(
                It.Is<List<ExecuteTradeServiceRequest>>(list =>
                    list.Count == 1 &&
                    list[0].Asset == "1" &&
                    list[0].Price == 1 &&
                    list[0].Quantity == 1 &&
                    list[0].UserId == 1
                ),
                cancellationToken
            ), Times.Once);
        }
    }
}