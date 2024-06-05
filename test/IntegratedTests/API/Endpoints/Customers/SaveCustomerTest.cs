using System.Net.Http.Json;
using API.Endpoints.Customers.SaveCustomer;
using AutoFixture;

namespace IntegratedTests.API.Endpoints.Customers;

public class SaveCustomerTest : IClassFixture<ApplicationFactory>
{
    private readonly ApplicationFactory _factory;
    private readonly Fixture _fixture = new();

    public SaveCustomerTest(ApplicationFactory factory) =>
        _factory = factory;

    [Fact]
    public async Task ShouldCreateCustomersWithSuccess()
    {
        // Arrange
        var client = _factory.GetClient();
        var request = _fixture.Create<SaveCustomerRequest>();

        // Act
        var response = await client.PostAsJsonAsync(Routes.Customer, request);
        
        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ShouldUpdateCustomerWithSuccess()
    {
        // Arrange
        var client = _factory.GetClient();
        var customerId = Guid.Parse("fd95f0a4-54f5-492e-8eb6-32a871527ef7");
        var request = _fixture.Create<SaveCustomerRequest>();

        // Act
        var response = await client.PutAsJsonAsync($"{Routes.Customer}/{customerId}", request);
        
        // Assert
        response.EnsureSuccessStatusCode();
    }
}
