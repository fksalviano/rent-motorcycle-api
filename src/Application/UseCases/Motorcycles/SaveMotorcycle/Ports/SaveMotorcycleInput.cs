namespace Application.UseCases.Motorcycles.SaveMotorcycle.Ports;

public struct SaveMotorcycleInput
{
    public Guid? Id { get; }
    public int Year { get; } = default;
    public string Model { get; } = string.Empty;
    public string Plate { get; }

    public readonly bool IsUpdate => Id is not null;

    public SaveMotorcycleInput(int year, string model, string plate)
    {        
        Year = year;
        Model = model;
        Plate = plate;
    }

    public SaveMotorcycleInput(Guid? id, string plate)
    {
        Id = id;
        Plate = plate;        
    }    
}
