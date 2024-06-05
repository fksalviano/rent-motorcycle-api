using Application.UseCases.Rents.SaveRent.Ports;

namespace API.Endpoints.Rents.SaveRent;

public class SaveRentRequest
{    
    public Guid CustomerId { get; }
    public Guid MotorcycleId { get; }
    public int RentDays { get; }    
    public DateTime StartDate { get; }    

    public SaveRentRequest(Guid customerId, Guid motorcycleId, int rentDays, DateTime startDate)
    {        
        CustomerId = customerId;
        MotorcycleId = motorcycleId;
        RentDays = rentDays;        
        StartDate = startDate;        
    }

    public SaveRentInput ToInput() =>
        new(CustomerId, MotorcycleId, RentDays, StartDate);
}
