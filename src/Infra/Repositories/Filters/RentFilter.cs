namespace Infra.Repositories.Filters;

public class RentFilter
{
    public Guid? Id { get; }
    public Guid? CustomerId { get; }

    public RentFilter(Guid? id = null) => Id = id;    

    public RentFilter(Guid? id = null, Guid? customerId = null)
    {
        Id = id;
        CustomerId = customerId;
    }
}
