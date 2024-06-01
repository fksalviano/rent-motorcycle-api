using Application.UseCases.Rents.SaveRent.Ports;

namespace Application.UseCases.Rents.SaveRent.Abstractions;

public interface ISaveRentOutputPort
{
    void Created(SaveRentOutput output);
    void Updated(UpdateRentOutput output);
    
    void NotFound();
    void Invalid(string message);    
    void Error(string message);
}
