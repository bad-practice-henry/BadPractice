using System.Globalization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

namespace WebUI;

public static class ConfigureServices
{
    public static async Task AddWebUIServices(this IServiceCollection services, WebAssemblyHostBuilder builder)
    {
        services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        services.AddLocalization();

        var jsInterop = builder.Build().Services.GetRequiredService<IJSRuntime>();
        var appLanguage = await jsInterop.InvokeAsync<string>("appCulture.get");
        if (appLanguage != null)
        {
            var cultureInfo = new CultureInfo(appLanguage);
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
    }
}