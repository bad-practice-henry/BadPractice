#region

using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ISalaryCalculationService, SalaryCalculationService>();
        services.AddScoped<HttpClient>();
    }
}