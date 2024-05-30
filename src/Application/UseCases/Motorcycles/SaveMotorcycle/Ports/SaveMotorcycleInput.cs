namespace Application.UseCases.Motorcycles.SaveMotorcycle.Ports;

public class SaveMotorcycleInput
{
    public Guid? Id { get; set; }
    public int Year { get; }
    public string Model { get; }
    public string Plate { get; }

    public bool IsUpdate => Id is not null;

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
        Year = default;
        Model = string.Empty;
    }
}
