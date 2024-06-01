using Application.UseCases.Rents.SaveRent.Ports;
using Domain.Model;
using Domain.Model.Extensions;

namespace Application.UseCases.Rents.SaveRent.Extensions;

public static class SaveRentExtensions
{    
    public static SaveRentOutput ToOutput(this Rent rent) =>
        new(rent);

    public static UpdateRentOutput ToUpdateOutput(this Rent rent) =>
        new(rent.Id, rent.EndDate, rent.EndValue);

    public static string ToStrings(this IEnumerable<RentPlan>? plans) => string.Join(", ",
        plans?.Select(plan => $"{plan.RentDays} ({plan.DailyValue})") ?? [string.Empty]);    

    public static Rent ToRent(this SaveRentInput input, RentPlan? rentPlan = null, Rent? rentToUpdate = null) => new
    (
        input.Id ?? Guid.NewGuid(),
        input.CustomerId,
        input.MotorcycleId,
        input.RentDays,
        rentPlan?.DailyValue * input.RentDays ?? 0,
        input.StartDate,
        expectedEnd: input.StartDate.AddDays(rentPlan?.RentDays ?? 0),
        input.EndDate,
        endValue: input.EndDate is not null 
            ? rentPlan?.GetEndValue(rentToUpdate!, input.EndDate.Value)
            : null
    );    
}
