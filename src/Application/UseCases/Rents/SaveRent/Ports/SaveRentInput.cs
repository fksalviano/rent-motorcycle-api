using Domain.Model;

namespace Application.UseCases.Rents.SaveRent.Ports;

public struct SaveRentInput
{    
    public Guid? Id { get; }    
    public Guid CustomerId { get; }
    public Guid MotorcycleId { get; }
    public int RentDays { get; }    
    public DateTime StartDate { get; }    
    public DateTime? EndDate { get; } = null;

    public readonly bool IsUpdate => Id is not null;    

    public SaveRentInput(Guid customerId, Guid motorcycleId, int rentDays, DateTime startDate)
    {        
        CustomerId = customerId;
        MotorcycleId = motorcycleId;
        RentDays = rentDays;        
        StartDate = startDate;        
    }

    public SaveRentInput(Guid? id, DateTime endDate)
    {
        Id = id;
        EndDate = endDate;        
    }    
}
