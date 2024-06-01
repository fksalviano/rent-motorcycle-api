namespace Application.UseCases.Rents.GetRents.Ports;

public struct GetRentsInput
{
    public Guid? Id { get; }
    public Guid? CustomerId { get; }

    public readonly bool IsGetById => Id is not null;

    public GetRentsInput(Guid? id = null) => Id = id;

    public GetRentsInput(Guid? id = null, Guid? customerId = null)
    {
        Id = id;
        CustomerId = customerId;
    }
}
