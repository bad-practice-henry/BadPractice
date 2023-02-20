using Application;
using Infrastructure;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebUI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
await builder.Services.AddWebUIServices(builder);

await builder.Build().RunAsync();