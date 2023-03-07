#region

using Infrastructure.HttpClient;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructureServices(this IServiceCollection services, string hostEnvironmentBaseAddress)
    {
        services.AddScoped<ISalaryCalculationService, SalaryCalculationService>();
        services.AddHttpClient<LocalDataHttpClient>(client => { client.BaseAddress = new Uri(hostEnvironmentBaseAddress); });
    }
}