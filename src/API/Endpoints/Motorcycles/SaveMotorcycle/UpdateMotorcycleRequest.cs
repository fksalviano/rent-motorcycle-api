namespace API.Endpoints.Motorcycles.SaveMotorcycle;

public class UpdateMotorcycleRequest
{        
    public string Plate { get; }

    public UpdateMotorcycleRequest(string plate)
    {     
        Plate = plate;
    }
}
