using Moq;
using Moq.AutoMock;
using AutoFixture;
using Infra.Repositories.Abstractions;
using Application.UseCases.Customers.GetCustomers.Abstractions;
using Application.UseCases.Customers.GetCustomers.Ports;
using Infra.Repositories.Filters;
using Domain.Model;
using Application.UseCases.Customers.GetCustomers;

namespace UnitTests.Application.UseCases.Customers;

public class GetCustomersUseCaseTests
{
    private readonly Mock<ICustomerRepository> _repository;
    private readonly Mock<IGetCustomersOutputPort> _outputPort;

    private readonly GetCustomersUseCase _sut;
    private readonly Fixture _fixture = new();

    public GetCustomersUseCaseTests()
    {
        var mocker = new AutoMocker();

        _repository = mocker.GetMock<ICustomerRepository>();
        _outputPort = mocker.GetMock<IGetCustomersOutputPort>();

        _sut = mocker.CreateInstance<GetCustomersUseCase>();
        _sut.SetOutputPort(_outputPort.Object);
    }

    [Fact]
    public async Task ShouldExecuteWithSuccess()
    {
        // Arrange
        var input = new GetCustomersInput();

        _repository
            .Setup(repo => repo.GetCustomers(It.IsAny<CustomerFilter>()))
            .ReturnsAsync(_fixture.CreateMany<Customer>());

        // Act
        await _sut.ExecuteAsync(input);

        // Assert
        _outputPort
            .Verify(output => output.Ok(It.IsAny<GetCustomersOutput>()));
    }

    [Fact]
    public async Task ShouldExecuteWithSuccessById()
    {
        // Arrange
        var input = new GetCustomersInput(_fixture.Create<Guid>());

        _repository
            .Setup(repo => repo.GetCustomers(It.IsAny<CustomerFilter>()))
            .ReturnsAsync(_fixture.CreateMany<Customer>(1));

        // Act
        await _sut.ExecuteAsync(input);

        // Assert
        _outputPort
            .Verify(output => output.Ok(It.IsAny<GetCustomersOutputById>()));
    }

    [Fact]
    public async Task ShouldExecuteWithNotFound()
    {
        // Arrange
        var input = _fixture.Create<GetCustomersInput>();

        _repository
            .Setup(repo => repo.GetCustomers(It.IsAny<CustomerFilter>()))
            .ReturnsAsync(_fixture.CreateMany<Customer>(0));

        // Act
        await _sut.ExecuteAsync(input);

        // Assert
        _outputPort
            .Verify(output => output.NotFound());
    }

    [Fact]
    public async Task ShouldExecuteWithError()
    {
        // Arrange
        var input = _fixture.Create<GetCustomersInput>();

        _repository
            .Setup(repo => repo.GetCustomers(It.IsAny<CustomerFilter>()))
            .ReturnsAsync((IEnumerable<Customer>?)null);


        // Act        
        await _sut.ExecuteAsync(input);

        // Assert
        _outputPort
            .Verify(output => output.Error(It.IsAny<string>()));
    }
}
