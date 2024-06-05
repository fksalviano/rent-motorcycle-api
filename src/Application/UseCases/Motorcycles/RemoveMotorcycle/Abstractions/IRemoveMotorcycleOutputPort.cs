using Application.UseCases.Motorcycles.RemoveMotorcycle.Ports;

namespace Application.UseCases.Motorcycles.RemoveMotorcycle.Abstractions;

public interface IRemoveMotorcycleOutputPort
{
    void Ok();    
    void NotFound();
    void Invalid(string message);
    void Error(string message);
}
