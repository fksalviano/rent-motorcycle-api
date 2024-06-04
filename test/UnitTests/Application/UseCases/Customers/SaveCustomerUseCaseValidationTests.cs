using Moq;
using Moq.AutoMock;
using AutoFixture;
using Infra.Repositories.Abstractions;
using Application.UseCases.Customers.SaveCustomer.Abstractions;
using Application.UseCases.Customers.SaveCustomer.Ports;
using Domain.Model;
using Application.UseCases.Customers.SaveCustomer;
using Infra.Repositories.Filters;

namespace UnitTests.Application.UseCases.Customers;

public class SaveCustomerUseCaseValidationTests
{
    private readonly Mock<ICustomerRepository> _repository;
    private readonly Mock<ISaveCustomerUseCase> _useCase;
    private readonly Mock<ISaveCustomerOutputPort> _outputPort;

    private readonly SaveCustomerUseCaseValidation _sut;
    private readonly Fixture _fixture = new();

    public SaveCustomerUseCaseValidationTests()
    {
        var mocker = new AutoMocker();

        _repository = mocker.GetMock<ICustomerRepository>();
        _outputPort = mocker.GetMock<ISaveCustomerOutputPort>();
        _useCase = mocker.GetMock<ISaveCustomerUseCase>();

        _sut = mocker.CreateInstance<SaveCustomerUseCaseValidation>();
        _sut.SetOutputPort(_outputPort.Object);
    }

    [Fact]
    public async Task ShouldExecuteWithSuccess()
    {                
        var input = _fixture.Create<SaveCustomerInput>();

        _repository
            .Setup(repo => repo.GetCustomers(It.IsAny<CustomerFilter>()))
            .ReturnsAsync(_fixture.CreateMany<Customer>(0));

        await _sut.ExecuteAsync(input);

        _useCase
            .Verify(useCase => useCase.ExecuteAsync(input));
    }

    public static TheoryData<string, string, DateTime, int> InvalidData = new()
    {
        {"", "123", DateTime.Now, 123 },
        {"name", "", DateTime.Now, 123 },
        {"name", "123", default, 123 },
        {"name", "123", DateTime.Now, 0 }
    };

    [Theory]
    [MemberData(nameof(InvalidData))]
    public async Task ShouldExecuteWithInvalid(string name, string taxId, DateTime bornDate, int licenseNumber)
    {
        var input = new SaveCustomerInput(name, taxId, bornDate, licenseNumber, default);

        _repository
            .Setup(repo => repo.GetCustomers(It.IsAny<CustomerFilter>()))
            .ReturnsAsync(_fixture.CreateMany<Customer>(1));

        await _sut.ExecuteAsync(input);

        _outputPort
            .Verify(output => output.Invalid(It.IsAny<string>()));
    }

    [Fact]
    public async Task ShouldExecuteWithError()
    {
        var input = _fixture.Create<SaveCustomerInput>();        

        _repository
            .Setup(repo => repo.GetCustomers(It.IsAny<CustomerFilter>()))
            .ReturnsAsync((IEnumerable<Customer>?)null);

        await _sut.ExecuteAsync(input);

        _outputPort
            .Verify(output => output.Error(It.IsAny<string>()));
    }

    [Fact]
    public async Task ShouldExecuteWithExistsTaxId()
    {
        var input = _fixture.Create<SaveCustomerInput>();        

        _repository
            .Setup(repo => repo.GetCustomers(It.Is<CustomerFilter>(filter => filter.TaxId != null)))
            .ReturnsAsync(_fixture.CreateMany<Customer>(1));

        _repository
            .Setup(repo => repo.GetCustomers(It.Is<CustomerFilter>(filter => filter.DriverLicenseNumber != null)))
            .ReturnsAsync(_fixture.CreateMany<Customer>(0));

        await _sut.ExecuteAsync(input);

        _outputPort
            .Verify(output => output.Invalid(It.IsAny<string>()));
    }

    [Fact]
    public async Task ShouldExecuteWithExistsLicenseNumber()
    {
        var input = _fixture.Create<SaveCustomerInput>();        

        _repository
            .Setup(repo => repo.GetCustomers(It.Is<CustomerFilter>(filter => filter.DriverLicenseNumber != null)))
            .ReturnsAsync(_fixture.CreateMany<Customer>(1));

        _repository
            .Setup(repo => repo.GetCustomers(It.Is<CustomerFilter>(filter => filter.TaxId != null)))
            .ReturnsAsync(_fixture.CreateMany<Customer>(0));        

        await _sut.ExecuteAsync(input);

        _outputPort
            .Verify(output => output.Invalid(It.IsAny<string>()));
    }
}
