using Microsoft.Extensions.DependencyInjection;

namespace JsonPatchSupport.AspNetCore;

public static class JsonPatchSupportSetup
{
    public static IServiceCollection AddJsonPatchSupport(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.InputFormatters.Insert(0, JsonPatchInputFormatterFactory.GetJsonPatchInputFormatter());
        });

        return services;
    }
}