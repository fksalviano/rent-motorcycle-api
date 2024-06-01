using Domain.Model;

namespace Application.UseCases.Rents.SaveRent.Ports;

public record UpdateRentOutput(Guid id, DateTime? EndDate, decimal? EndValue);
