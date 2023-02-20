using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;


namespace Application;

public static class ConfigureServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        var config = new TypeAdapterConfig();

        services.AddSingleton(config);
        services.AddSingleton<IMapper, ServiceMapper>();
    }
}