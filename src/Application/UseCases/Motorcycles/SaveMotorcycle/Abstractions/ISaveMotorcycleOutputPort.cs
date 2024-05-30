using Application.UseCases.Motorcycles.SaveMotorcycle.Ports;

namespace Application.UseCases.Motorcycles.SaveMotorcycle.Abstractions;

public interface ISaveMotorcycleOutputPort
{
    void Created(SaveMotorcycleOutput output);
    void Updated(UpdateMotorcycleOutput output);
    
    void NotFound();
    void Invalid(string message);    
    void Error(string message);
}
