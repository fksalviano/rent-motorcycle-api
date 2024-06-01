using Application.UseCases.Motorcycles.SaveMotorcycle.Ports;

namespace API.Endpoints.Motorcycles.SaveMotorcycle;

public class SaveMotorcycleRequest
{
    public int Year { get; }
    public string Model { get; }
    public string Plate { get; }

    public SaveMotorcycleRequest(int year, string model, string plate)
    {
        Year = year;
        Model = model;
        Plate = plate;
    }

    public SaveMotorcycleInput ToInput() =>
        new(Year, Model, Plate);
}
