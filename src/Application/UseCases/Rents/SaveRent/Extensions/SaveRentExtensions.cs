using Application.UseCases.Rents.SaveRent.Ports;
using Domain.Model;

namespace Application.UseCases.Rents.SaveRent.Extensions;

public static class SaveRentExtensions
{    
    public static SaveRentOutput ToOutput(this Rent rent) =>
        new(rent);

    public static UpdateRentOutput ToUpdateOutput(this Rent rent) =>
        new(rent.Id, rent.EndDate, rent.EndValue);

    public static string ToStrings(this IEnumerable<RentPlan> plans) => string.Join(", ",
        plans.Select(plan => $"{plan.RentDays} ({plan.RentValue})"));

    public static RentPlan? GetPlan(this IEnumerable<RentPlan>? plans, int rentDays) =>
        plans!.Where(plan => plan.RentDays == rentDays)?.FirstOrDefault();

    public static Rent ToRent(this SaveRentInput input, RentPlan rentPlan) => new
    (
        input.Id ?? Guid.NewGuid(),
        input.CustomerId,
        input.MotorcycleId,
        rentPlan.RentDays,
        rentPlan.RentValue,
        input.StartDate,
        expectedEnd: input.StartDate.AddDays(rentPlan.RentDays),
        input.EndDate,
        input.EndValue
    );
}
