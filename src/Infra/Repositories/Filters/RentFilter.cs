namespace Infra.Repositories.Filters;

public class RentFilter
{
    public Guid? Id { get; }
    public Guid? CustomerId { get; }
    public Guid? MotorcycleId { get; }

    public RentFilter(Guid? id = null) => Id = id;    

    public RentFilter(Guid? id = null, Guid? customerId = null, Guid? motorcycleId = null)
    {
        Id = id;
        CustomerId = customerId;
        MotorcycleId = motorcycleId;
    }
}
