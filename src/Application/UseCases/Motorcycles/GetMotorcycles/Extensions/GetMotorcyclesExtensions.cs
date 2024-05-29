using Application.UseCases.Motorcycles.GetMotorcycles.Ports;
using Domain.Model;

namespace Application.UseCases.Motorcycles.GetMotorcycles.Extensions;

public static class GetMotorcyclesExtensions
{
    public static GetMotorcyclesOutput ToOutput(this IEnumerable<Motorcycle> motorcycles) =>
        new(motorcycles);
}
