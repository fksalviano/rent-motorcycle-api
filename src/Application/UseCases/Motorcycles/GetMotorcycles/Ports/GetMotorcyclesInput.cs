namespace Application.UseCases.Motorcycles.GetMotorcycles.Ports;

public struct GetMotorcyclesInput
{
    public Guid? Id { get; }
    public string? Plate { get; }

    public readonly bool IsGetById => Id is not null;

    public GetMotorcyclesInput(string? plate)
    {        
        Plate = plate;
    }    

    public GetMotorcyclesInput(Guid? id) => Id = id;
}
