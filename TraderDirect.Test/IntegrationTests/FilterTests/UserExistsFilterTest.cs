using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TraderDirect.App.ApiModels.Requests;
using TraderDirect.Infrastructure.Databases.TraderDirectDb.Entities;
using TraderDirect.Infrastructure.Databases.TraderDirectDb;
using Microsoft.Extensions.DependencyInjection;

namespace TraderDirect.Test.IntegrationTests.FilterTests;
public class UserExistsFilterTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public UserExistsFilterTest(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _factory = factory;
    }

    [Fact]
    public async Task CreateUser_ExistingUser_ShouldReturnConflict()
    {
        using var scope = _factory.Services.CreateScope();
        TraderDirectDbContext dbContext = scope.ServiceProvider.GetRequiredService<TraderDirectDbContext>();
        UserEntity existingUser = new UserEntity { Email = "existinguser@example.com" };
        dbContext.Users.Add(existingUser);
        await dbContext.SaveChangesAsync();

        CreateUserRequest request = new CreateUserRequest
        {
            Email = existingUser.Email
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/user", request);

        // Assert
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains($"User with email address {request.Email} already exists", content);
    }
}
