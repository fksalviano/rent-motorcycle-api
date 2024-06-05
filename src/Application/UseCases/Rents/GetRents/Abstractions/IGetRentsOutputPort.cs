using Application.UseCases.Rents.GetRents.Ports;

namespace Application.UseCases.Rents.GetRents.Abstractions;

public interface IGetRentsOutputPort
{
    void Ok(GetRentsOutput output);
    void Ok(GetRentsOutputById output);    
    void NotFound();
    void Error(string message);
}
