namespace Application.UseCases.Motorcycles.SaveMotorcycle.Ports;

public struct SaveMotorcycleInput
{
    public Guid? Id { get; }
    public int Year { get; }
    public string Model { get; }
    public string Plate { get; }

    public readonly bool IsUpdate => Id is not null;

    public SaveMotorcycleInput(int year, string model, string plate, Guid? id = null)
    {
        Id = id;
        Year = year;
        Model = model;
        Plate = plate;
    }

    public SaveMotorcycleInput(Guid? id, string plate)
    {
        Id = id;
        Plate = plate;

        // fields not used on update
        Year = default;
        Model = string.Empty;
    }
}
