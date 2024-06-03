using Application.UseCases.Motorcycles.SaveMotorcycleNotify.Ports;

namespace Application.UseCases.Motorcycles.SaveMotorcycleNotify.Abstractions;

public interface ISaveMotorcycleNotifyOutputPort
{
    void OK(SaveMotorcycleNotifyOutput output);
    void Error(string message);
}
