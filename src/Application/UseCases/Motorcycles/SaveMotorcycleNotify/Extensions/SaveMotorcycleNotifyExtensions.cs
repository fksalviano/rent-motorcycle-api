using Application.UseCases.Motorcycles.SaveMotorcycleNotify.Ports;
using Domain.Model;

namespace Application.UseCases.Motorcycles.SaveMotorcycleNotify.Extensions;

public static class SaveMotorcycleNotifyExtensions
{
    public static MotorcycleNotify ToMotorcycleNotify(this SaveMotorcycleNotifyInput input) =>
        new(input.MotorcycleId, input.CreatedAt);

    public static SaveMotorcycleNotifyOutput ToOutput(this MotorcycleNotify motorcycleNotify) =>
        new(motorcycleNotify.MotorcycleId);
}
