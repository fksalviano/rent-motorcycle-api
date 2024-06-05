using Application.UseCases.Rents.SaveRent.Ports;

namespace API.Endpoints.Rents.SaveRent;

public class UpdateRentRequest
{
    public DateTime EndDate { get; }

    public UpdateRentRequest(DateTime endDate)
    {
        EndDate = endDate;        
    }   

    public SaveRentInput ToInput(Guid id) => new(id, EndDate);
}
