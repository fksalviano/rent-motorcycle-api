namespace Domain.Model;

public class MotorcycleNotify
{
    public Guid MotorcycleId { get; }
    public DateTime CreatedAt { get; }


    public MotorcycleNotify(Guid motorcycleId, DateTime createdAt)
    {
        MotorcycleId = motorcycleId;
        CreatedAt = createdAt;
    }
}
