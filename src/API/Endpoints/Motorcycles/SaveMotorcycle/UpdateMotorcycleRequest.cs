using Application.UseCases.Motorcycles.SaveMotorcycle.Ports;

namespace API.Endpoints.Motorcycles.SaveMotorcycle;

public class UpdateMotorcycleRequest
{        
    public string Plate { get; }

    public UpdateMotorcycleRequest(string plate)
    {     
        Plate = plate;
    }

    public SaveMotorcycleInput ToInput(Guid id) =>
        new(id, Plate);
}
