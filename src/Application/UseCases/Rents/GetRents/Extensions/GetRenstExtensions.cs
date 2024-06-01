using Application.UseCases.Rents.GetRents.Ports;
using Domain.Configuration;
using Domain.Model;
using Domain.Model.Extensions;
using Infra.Repositories.Filters;

namespace Application.UseCases.Rents.GetRents.Extensions;

public static class GetRentsExtensions
{
    public static RentFilter ToFilter(this GetRentsInput input) =>
        new(input.Id, input.CustomerId);

    public static GetRentsOutput ToOutput(this IEnumerable<Rent> rents) =>
        new(rents);    

    public static GetRentsOutputById ToOutput(this Rent rent, GetRentsInput input, RentConfiguration rentConfig)
    {
        if (input.IsEndPreview)
        {
            var endDatePreview = input.EndDatePreview!.Value;

            var plan = rentConfig.Plans.GetPlan(rent.RentDays);
            var endValuePreview = plan!.GetEndValue(rent, endDatePreview);

            return new(rent, new(endDatePreview, endValuePreview));
        }
        else
            return new(rent);
    }        
}
