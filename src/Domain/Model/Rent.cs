namespace Domain.Model;

public struct Rent
{    
    public Guid Id { get; }
    public Guid CustomerId { get; }
    public Guid MotorcycleId { get; }
    public int RentDays { get; }
    public decimal RentValue { get; }
    public DateTime StartDate { get; }
    public DateTime ExpectedEnd { get; }
    public DateTime? EndDate { get; }
    public decimal? EndValue { get; }

    public Rent(Guid id, Guid customerId, Guid motorcycleId, int rentDays, decimal rentValue, 
        DateTime startDate, DateTime expectedEnd, DateTime? endDate = null, decimal? endValue = null)
    {
        Id = id;
        CustomerId = customerId;
        MotorcycleId = motorcycleId;
        RentDays = rentDays;
        RentValue = rentValue;
        StartDate = startDate;
        ExpectedEnd = expectedEnd;
        EndDate = endDate;
        EndValue = endValue;
    }
}
