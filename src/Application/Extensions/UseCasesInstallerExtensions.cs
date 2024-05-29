using System.Diagnostics.CodeAnalysis;
using Application.UseCases.Motorcycles.GetMotorcycles;
using Application.UseCases.Motorcycles.GetMotorcycles.Abstractions;
using Application.UseCases.Motorcycles.SaveMotorcycle;
using Application.UseCases.Motorcycles.SaveMotorcycle.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

[ExcludeFromCodeCoverage]
public static class UseCasesInstallerExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services) =>
        services
            .AddScoped<IGetMotorcyclesUseCase, GetMotorcyclesUseCase>()
                        
            .AddScoped<SaveMotorcycleUseCase>()
            .AddScoped<ISaveMotorcycleUseCase>(provider =>             
                new SaveMotorcycleUseCaseValidation(provider.GetRequiredService<SaveMotorcycleUseCase>()));
}
