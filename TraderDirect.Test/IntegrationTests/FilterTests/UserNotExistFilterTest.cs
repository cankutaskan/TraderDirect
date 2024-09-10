using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net;
using System.Net.Http.Json;
using TraderDirect.App.ApiModels.Requests;
using TraderDirect.Infrastructure.Providers;

namespace TraderDirect.Test.IntegrationTests;
public class UserNotExistFilterTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly Mock<IRabbitMqProvider> _rabbitMqMock;

    public UserNotExistFilterTest(CustomWebApplicationFactory<Program> factory)
    {
        _rabbitMqMock = new Mock<IRabbitMqProvider>();
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ExecuteTrade_WithNonExistingUser_ShouldReturnNotFound()
    {
        // Arrange
        ExecuteTradesRequest request = new()
        {
            UserId = 1,
            Trades = new List<CreateTradeRequest>
            {
                new CreateTradeRequest { Asset = "1", Quantity = 10, Price = 12 }
            }
        };

        // Act
        HttpResponseMessage response = await _client.PostAsJsonAsync("/api/trades", request);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains($"User with ID {request.UserId} does not exist", content);
    }
}

