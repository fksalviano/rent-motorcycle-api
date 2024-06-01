using Application.UseCases.Rents.SaveRent.Ports;

namespace API.Endpoints.Rents.SaveRent;

public class UpdateRentRequest
{
    public DateTime EndDate { get; }
    public decimal Endvalue { get; }

    public UpdateRentRequest(DateTime endDate, decimal endvalue)
    {
        EndDate = endDate;
        Endvalue = endvalue;
    }   

    public SaveRentInput ToInput(Guid id) => new(id, EndDate, Endvalue);
}
