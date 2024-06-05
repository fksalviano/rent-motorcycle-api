using Microsoft.VisualBasic;

namespace Domain.Model.Extensions;

public static class RentPlanExtenions
{
    public static decimal GetEndValue(this RentPlan plan, Rent rent, DateTime endDate)
    {        
        decimal fineValue = 0;

        if (endDate < rent.ExpectedEnd && plan.PercentageFine > 0)
        {
            var percent = plan.PercentageFine / 100;
            var daysNotRent = rent.ExpectedEnd.Subtract(endDate).Days;
            var daysNotRentValue = plan.DailyValue * daysNotRent;
            fineValue += percent * daysNotRentValue;
        }

        int rentDays;

        if (endDate > rent.ExpectedEnd)
        {
            decimal additionalDailyValue = 50;
            var additionalDays = endDate.Subtract(rent.ExpectedEnd).Days;            
            fineValue += additionalDailyValue * additionalDays;

            rentDays = rent.ExpectedEnd.Subtract(rent.StartDate).Days;
        }
        else
            rentDays = endDate.Subtract(rent.StartDate).Days;

        var endValue = rentDays * plan.DailyValue;

        endValue += fineValue;
        return endValue;
    }

    public static RentPlan? GetPlan(this IEnumerable<RentPlan>? plans, int rentDays) =>
        plans?.FirstOrDefault(plan => plan.RentDays == rentDays);
}
