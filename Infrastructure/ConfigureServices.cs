using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<ISalaryCalculationService, SalaryCalculationService>();
    }
}