using Moq;
using Moq.AutoMock;
using AutoFixture;
using Infra.Repositories.Abstractions;
using Application.UseCases.Customers.SaveCustomer.Abstractions;
using Application.UseCases.Customers.SaveCustomer.Ports;
using Domain.Model;
using Application.UseCases.Customers.SaveCustomer;
using API.Endpoints.Customers.SaveCustomer;

namespace UnitTests.Application.UseCases.Customers;

public class SaveCustomerUseCaseTests
{
    private readonly Mock<ICustomerRepository> _repository;
    private readonly Mock<ISaveCustomerOutputPort> _outputPort;

    private readonly SaveCustomerUseCase _sut;
    private readonly Fixture _fixture = new();

    public SaveCustomerUseCaseTests()
    {
        var mocker = new AutoMocker();

        _repository = mocker.GetMock<ICustomerRepository>();
        _outputPort = mocker.GetMock<ISaveCustomerOutputPort>();

        _sut = mocker.CreateInstance<SaveCustomerUseCase>();
        _sut.SetOutputPort(_outputPort.Object);
    }

    [Fact]
    public async Task ShouldExecuteWithSuccessCreated()
    {
        var request = _fixture.Create<SaveCustomerRequest>();
        var input = request.ToInput();

        _repository
            .Setup(repo => repo.CreateCustomer(It.IsAny<Customer>()))
            .ReturnsAsync(1);

        await _sut.ExecuteAsync(input);

        _outputPort
            .Verify(output => output.Created(It.IsAny<SaveCustomerOutput>()));
    }

    [Fact]
    public async Task ShouldExecuteWithSuccessUpdated()
    {
        var request = _fixture.Create<SaveCustomerRequest>();
        var input = request.ToInput(_fixture.Create<Guid>());

        _repository
            .Setup(repo => repo.UpdateCustomer(It.IsAny<Customer>()))
            .ReturnsAsync(1);

        await _sut.ExecuteAsync(input);

        _outputPort
            .Verify(output => output.Updated(It.IsAny<SaveCustomerOutput>()));
    }

    [Fact]
    public async Task ShouldExecuteWithNotFound()
    {
        var request = _fixture.Create<SaveCustomerRequest>();
        var input = request.ToInput(_fixture.Create<Guid>());

        _repository
            .Setup(repo => repo.UpdateCustomer(It.IsAny<Customer>()))
            .ReturnsAsync(0);

        await _sut.ExecuteAsync(input);

        _outputPort
            .Verify(output => output.NotFound());
    }

    [Fact]
    public async Task ShouldExecuteWithError()
    {
        var request = _fixture.Create<SaveCustomerRequest>();
        var input = request.ToInput(_fixture.Create<Guid>());

        _repository
            .Setup(repo => repo.CreateCustomer(It.IsAny<Customer>()))
            .ReturnsAsync((int?)null);

        await _sut.ExecuteAsync(input);

        _outputPort
            .Verify(output => output.Error(It.IsAny<string>()));
    }
}
