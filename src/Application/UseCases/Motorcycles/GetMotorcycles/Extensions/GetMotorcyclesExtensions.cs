using Application.UseCases.Motorcycles.GetMotorcycles.Ports;
using Domain.Model;
using Infra.Repositories.Filters;

namespace Application.UseCases.Motorcycles.GetMotorcycles.Extensions;

public static class GetMotorcyclesExtensions
{
    public static MotorcycleFilter ToFilter(this GetMotorcyclesInput input) =>
        new (input.Id, input.Plate);    

    public static GetMotorcyclesOutput ToOutput(this IEnumerable<Motorcycle> motorcycles) =>
        new(motorcycles);

    public static GetMotorcyclesOutputById ToOutput(this Motorcycle motorcycle) =>
        new(motorcycle);
}
