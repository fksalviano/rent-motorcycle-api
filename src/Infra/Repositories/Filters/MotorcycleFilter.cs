namespace Infra.Repositories.Filters;

public struct MotorcycleFilter
{
    public Guid? Id { get; }
    public string? Plate { get; }

    public MotorcycleFilter(Guid? id = null, string? plate = null)
    {
        Id = id;
        Plate = plate;
    }

    public MotorcycleFilter(Guid? id) => Id = id;

    public MotorcycleFilter(string? plate) => Plate = plate;    
}
