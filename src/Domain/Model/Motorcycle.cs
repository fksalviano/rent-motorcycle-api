namespace Domain.Model;

public struct Motorcycle
{
    public Guid Id { get; }
    public int Year { get; }
    public string Model { get; }
    public string Plate { get; }

    public Motorcycle(Guid id, int year, string model, string plate)
    {
        Id = id;
        Year = year;
        Model = model;
        Plate = plate;
    }
}
