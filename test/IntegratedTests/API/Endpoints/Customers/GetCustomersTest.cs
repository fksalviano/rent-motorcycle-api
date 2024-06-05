namespace IntegratedTests.API.Endpoints.Customers;

public class GetCustomersTest : IClassFixture<ApplicationFactory>
{
    private readonly ApplicationFactory _factory;

    public GetCustomersTest(ApplicationFactory factory) =>
        _factory = factory;

    [Fact]
    public async Task ShouldGetCustomersWithSuccess()
    {
        // Arrange
        var client = _factory.GetClient();

        // Act
        var response = await client.GetAsync(Routes.Customer);
        
        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ShouldGetCustomerByIdWithSuccess()
    {
        // Arrange
        var client = _factory.GetClient();
        var customerId = Guid.Parse("fd95f0a4-54f5-492e-8eb6-32a871527ef7");

        // Act
        var response = await client.GetAsync($"{Routes.Customer}/{customerId}");
        
        // Assert
        response.EnsureSuccessStatusCode();
    }
}
