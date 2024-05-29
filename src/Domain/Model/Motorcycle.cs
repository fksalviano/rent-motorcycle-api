namespace Domain.Model;

public class Motorcycle
{
    public Guid Id { get; }
    public int Year { get; }
    public string Model { get; }
    public string Plate { get; }

    public Motorcycle(int year, string model, string plate)
    {
        Id = Guid.NewGuid();
        Year = year;
        Model = model;
        Plate = plate;
    }
}
