using Application.UseCases.Rents.SaveRent.Ports;

namespace Application.UseCases.Rents.SaveRent.Abstractions;

public interface ISaveRentUseCase
{
    Task ExecuteAsync(SaveRentInput input);

    void SetOutputPort(ISaveRentOutputPort outputPort);
}
