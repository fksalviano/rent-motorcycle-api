using Application.UseCases.Motorcycles.SaveMotorcycle.Ports;
using Domain.Model;

namespace Application.UseCases.Motorcycles.SaveMotorcycle.Extensions;

public static class SaveMotorcycleExtensions
{
    public static SaveMotorcycleOutput ToSaveOutput(this Motorcycle motorcycle) =>
        new(motorcycle);

    public static UpdateMotorcycleOutput ToUpdateOutput(this Motorcycle motorcycle) =>
        new(motorcycle.Id, motorcycle.Plate);

    public static Motorcycle ToMotorcycle(this SaveMotorcycleInput input) =>
        new(input.Id ?? Guid.NewGuid(), input.Year, input.Model, input.Plate);    
}
