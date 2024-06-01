using Application.UseCases.Rents.GetRents.Ports;

namespace Application.UseCases.Rents.GetRents.Abstractions;

public interface IGetRentsUseCase
{
    Task ExecuteAsync(GetRentsInput input);

    void SetOutputPort(IGetRentsOutputPort outputPort);
}
