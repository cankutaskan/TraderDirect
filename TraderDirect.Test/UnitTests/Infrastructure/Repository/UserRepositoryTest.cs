using Microsoft.EntityFrameworkCore;
using TraderDirect.Infrastructure.Databases.TraderDirectDb.Repositories;
using TraderDirect.Infrastructure.Databases.TraderDirectDb;
using Microsoft.EntityFrameworkCore.Internal;
using TraderDirect.Infrastructure.Databases.TraderDirectDb.Entities;

namespace TraderDirect.Test.UnitTests.Infrastructure.Repository;
public class UserRepositoryTest
{
    private readonly TraderDirectDbContext _dbContext;
    private readonly UserRepository _userRepository;

    public UserRepositoryTest()
    {
        _dbContext = DbContextFactory.CreateInMemoryDbContext();
        _userRepository = new UserRepository(_dbContext);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldAddUserToDatabase_UserAddedIdReturned()
    {
        // Arrange
        string email = "test@example.com";
        CancellationToken cancellationToken = new();

        // Act
        int userId = await _userRepository.CreateUserAsync(email, cancellationToken);

        // Assert
        UserEntity? newUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

        Assert.NotNull(newUser);
        Assert.Equal(email, newUser.Email);
        Assert.Equal(userId, newUser.Id);
    }
}

