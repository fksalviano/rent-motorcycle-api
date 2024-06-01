using Application.UseCases.Rents.GetRents.Ports;
using Domain.Model;
using Infra.Repositories.Filters;

namespace Application.UseCases.Rents.GetRents.Extensions;

public static class GetRentsExtensions
{
    public static RentFilter ToFilter(this GetRentsInput input) =>
        new(input.Id, input.CustomerId);

    public static GetRentsOutput ToOutput(this IEnumerable<Rent> Rents) =>
        new(Rents);

    public static GetRentsOutputById ToOutput(this Rent Rent) =>
        new(Rent);
}
