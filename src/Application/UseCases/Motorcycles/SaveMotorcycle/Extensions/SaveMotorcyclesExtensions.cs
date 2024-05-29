using Application.UseCases.Motorcycles.SaveMotorcycle.Ports;
using Domain.Model;

namespace Application.UseCases.Motorcycles.SaveMotorcycle.Extensions;

public static class SaveMotorcycleExtensions
{
    public static SaveMotorcycleOutput ToOutput(this Motorcycle motorcycle) =>
        new(motorcycle);

    public static Motorcycle ToMotorcycle(this SaveMotorcycleInput input) =>
        new(input.Year, input.Model, input.Plate);
}
