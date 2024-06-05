using Application.UseCases.Motorcycles.GetMotorcycles.Ports;

namespace Application.UseCases.Motorcycles.GetMotorcycles.Abstractions;

public interface IGetMotorcyclesOutputPort
{
    void Ok(GetMotorcyclesOutput output);
    void Ok(GetMotorcyclesOutputById output);    
    void NotFound();
    void Error(string message);
}
