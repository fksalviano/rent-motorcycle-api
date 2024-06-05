using Moq;
using Moq.AutoMock;
using AutoFixture;
using Application.UseCases.Customers.SaveCustomerDocument.Abstractions;
using Application.UseCases.Customers.SaveCustomerDocument.Ports;
using Application.UseCases.Customers.SaveCustomerDocument;

namespace UnitTests.Application.UseCases.Customers;

public class SaveCustomerDocumentUseCaseTests
{
    private readonly Mock<ISaveCustomerDocumentOutputPort> _outputPort;

    private readonly SaveCustomerDocumentUseCase _sut;
    private readonly Fixture _fixture = new();

    public SaveCustomerDocumentUseCaseTests()
    {
        var mocker = new AutoMocker();
        _outputPort = mocker.GetMock<ISaveCustomerDocumentOutputPort>();

        _sut = mocker.CreateInstance<SaveCustomerDocumentUseCase>();
        _sut.SetOutputPort(_outputPort.Object);
    }

    [Fact]
    public async Task ShouldExecuteWithSuccess()
    {
        // Arrange
        var id = _fixture.Create<Guid>();
        var fileType = _fixture.Create<string>();

        var fileStream = GetFixtureStream();
        var input = new SaveCustomerDocumentInput(id, fileStream, fileType);

        // Act        
        await _sut.ExecuteAsync(input);
        
        // Assert
        _outputPort
            .Verify(output => output.Ok(It.IsAny<SaveCustomerDocumentOutput>()));
    }

    private Stream GetFixtureStream()
    {
        var stream = new MemoryStream();
        var writer =  new StreamWriter(stream);

        writer.Write(_fixture.Create<string>());
        writer.Flush();

        stream.Position = 0;
        return stream;
    }
}
