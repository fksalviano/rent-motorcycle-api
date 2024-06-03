using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Infra.Repositories.Abstractions;
using Application.UseCases.Motorcycles.GetMotorcycles;
using Application.UseCases.Motorcycles.GetMotorcycles.Abstractions;
using Application.UseCases.Motorcycles.SaveMotorcycle;
using Application.UseCases.Motorcycles.SaveMotorcycle.Abstractions;
using Application.UseCases.Motorcycles.SaveMotorcycleNotify;
using Application.UseCases.Motorcycles.SaveMotorcycleNotify.Abstractions;
using Application.UseCases.Motorcycles.RemoveMotorcycle;
using Application.UseCases.Motorcycles.RemoveMotorcycle.Abstractions;
using Application.UseCases.Customers.GetCustomers.Abstractions;
using Application.UseCases.Customers.GetCustomers;
using Application.UseCases.Customers.SaveCustomer;
using Application.UseCases.Customers.SaveCustomer.Abstractions;
using Application.UseCases.Rents.GetRents.Abstractions;
using Application.UseCases.Rents.GetRents;
using Application.UseCases.Rents.SaveRent;
using Application.UseCases.Rents.SaveRent.Abstractions;


namespace Application.Extensions;

[ExcludeFromCodeCoverage]
public static class UseCasesInstallerExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services) =>
        services
            // Motorcycles
            .AddScoped<IGetMotorcyclesUseCase, GetMotorcyclesUseCase>()

            .AddScoped<SaveMotorcycleUseCase>()
            .AddScoped<ISaveMotorcycleUseCase>(provider => new SaveMotorcycleUseCaseValidation(
                provider.GetRequiredService<SaveMotorcycleUseCase>(), provider.GetRequiredService<IMotorcycleRepository>()))

            .AddScoped<ISaveMotorcycleNotifyUseCase, SaveMotorcycleNotifyUseCase>()

            .AddScoped<RemoveMotorcycleUseCase>()
            .AddScoped<IRemoveMotorcycleUseCase>(provider => new RemoveMotorcycleUseCaseValidation(
                provider.GetRequiredService<RemoveMotorcycleUseCase>(), provider.GetRequiredService<IRentRepository>()))

            // Customers
            .AddScoped<IGetCustomersUseCase, GetCustomersUseCase>()

            .AddScoped<SaveCustomerUseCase>()
            .AddScoped<ISaveCustomerUseCase>(provider => new SaveCustomerUseCaseValidation(
                provider.GetRequiredService<SaveCustomerUseCase>(), provider.GetRequiredService<ICustomerRepository>()))

             // Rents
            .AddScoped<IGetRentsUseCase, GetRentsUseCase>()

            .AddScoped<SaveRentUseCase>()
            .AddScoped<ISaveRentUseCase>(provider => new SaveRentUseCaseValidation(
                provider.GetRequiredService<SaveRentUseCase>(), provider.GetRequiredService<IRentRepository>(),
                provider.GetRequiredService<ICustomerRepository>(), provider.GetRequiredService<IMotorcycleRepository>()));
}
