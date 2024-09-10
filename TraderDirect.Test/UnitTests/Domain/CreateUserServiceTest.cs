using Moq;
using TraderDirect.Domain.Ports.Repositories;
using TraderDirect.Domain.Services;

namespace TraderDirect.Test.UnitTests.Domain;
public class CreateUserServiceTest
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly CreateUserService _createUserService;

    public CreateUserServiceTest()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _createUserService = new CreateUserService(_mockUserRepository.Object);
    }

    [Fact]
    public async Task HandleAsync_ShouldCallCreateUserAsync_UserCreatedIdReturned()
    {
        // Arrange
        string email = "test@example.com";
        CancellationToken cancellationToken = new();

        _mockUserRepository
            .Setup(repo => repo.CreateUserAsync(email, cancellationToken))
            .ReturnsAsync(1);

        // Act
        int result = await _createUserService.HandleAsync(email, cancellationToken);

        // Assert
        Assert.Equal(1, result);
        _mockUserRepository.Verify(repo => repo.CreateUserAsync(email, cancellationToken), Times.Once);
    }
}

