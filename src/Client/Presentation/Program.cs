using FinanceLab.Client.Application.Extensions;
using FinanceLab.Client.Infrastructure.Extensions;
using FinanceLab.Client.Presentation.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Logging.SetMinimumLevel(builder.HostEnvironment.IsDevelopment() ? LogLevel.Debug : LogLevel.Warning);

builder.RootComponents.Add<AppComponent>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClientService(builder.HostEnvironment.BaseAddress);
builder.Services.AddStateContainerService();
builder.Services.AddJsonSerializerOptions();
builder.Services.AddAuthorization();
builder.Services.AddMudServices();

await builder.Build().RunAsync();