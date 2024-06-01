namespace Application.UseCases.Rents.GetRents.Ports;

public struct GetRentsInput
{
    public Guid? Id { get; }
    public DateTime? EndDatePreview { get; set; }
    public Guid? CustomerId { get; }    

    public readonly bool IsGetById => Id is not null;
    public readonly bool IsEndPreview => EndDatePreview is not null;

    public GetRentsInput(Guid? id = null, DateTime? endDatePreview = null) 
    {
        Id = id;
        EndDatePreview = endDatePreview;
    }

    public GetRentsInput(Guid? id = null, Guid? customerId = null, DateTime? endDatePreview = null)
    {
        Id = id;
        CustomerId = customerId;
        EndDatePreview = endDatePreview;
    }    
}
