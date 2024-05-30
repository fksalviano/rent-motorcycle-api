using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Infra.Repositories.Abstractions;
using Application.UseCases.Motorcycles.GetMotorcycles;
using Application.UseCases.Motorcycles.GetMotorcycles.Abstractions;
using Application.UseCases.Motorcycles.SaveMotorcycle;
using Application.UseCases.Motorcycles.SaveMotorcycle.Abstractions;
using Application.UseCases.Motorcycles.RemoveMotorcycle;
using Application.UseCases.Motorcycles.RemoveMotorcycle.Abstractions;


namespace Application.Extensions;

[ExcludeFromCodeCoverage]
public static class UseCasesInstallerExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services) =>
        services
            .AddScoped<IGetMotorcyclesUseCase, GetMotorcyclesUseCase>()

            .AddScoped<SaveMotorcycleUseCase>()
            .AddScoped<ISaveMotorcycleUseCase>(provider =>
                new SaveMotorcycleUseCaseValidation(
                    provider.GetRequiredService<SaveMotorcycleUseCase>(),
                    provider.GetRequiredService<IMotorcycleRepository>()))
                    
            .AddScoped<RemoveMotorcycleUseCase>()
            .AddScoped<IRemoveMotorcycleUseCase>(provider =>
                new RemoveMotorcycleUseCaseValidation(
                    provider.GetRequiredService<RemoveMotorcycleUseCase>(),
                    provider.GetRequiredService<IMotorcycleRepository>()));
}
