using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Notifications.Motorcycles.Abstractions;
using Application.Notifications.Motorcycles;

namespace Application.Extensions;

[ExcludeFromCodeCoverage]
public static class NotificationsInstallerExtensions
{
    public static IServiceCollection AddNotifications(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddScoped<IMotorcycleCreatedNotify, MotorcycleCreatedNotify>();
}
