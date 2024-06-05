using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions;

public static class SerializerConfigurationExtensions
{
    public static IServiceCollection ConfigureSerializer(this IServiceCollection services) =>
        services
            // AspNetCore.Http serializer options
            .ConfigureHttpJsonOptions(options =>
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()))

            // AspnetCore.Mvc serializer options
            .Configure<JsonOptions>(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
}
