namespace Application.UseCases.Motorcycles.SaveMotorcycleNotify.Ports;

public record SaveMotorcycleNotifyInput
(
    Guid MotorcycleId, 
    DateTime CreatedAt
);
