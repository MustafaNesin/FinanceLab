using System.Globalization;
using Blazored.LocalStorage;
using FinanceLab.Client.Application.Extensions;
using FinanceLab.Client.Infrastructure.Extensions;
using FinanceLab.Client.Presentation.Components;
using FinanceLab.Client.Presentation.Resources;
using FinanceLab.Shared.Application.Abstractions;
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
builder.Services.AddLocalization()
    .AddTransient<ISharedResources, SharedResources>();
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();

var host = builder.Build();

CultureInfo cultureInfo;

var localStorage = host.Services.GetRequiredService<ILocalStorageService>();
var cultureName = await localStorage.GetItemAsStringAsync("Culture");

if (cultureName is not null)
    cultureInfo = new CultureInfo(cultureName);
else
{
    const string defaultCultureName = "en-US";
    cultureInfo = new CultureInfo(defaultCultureName);
    await localStorage.SetItemAsStringAsync("Culture", defaultCultureName);
}

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

await host.RunAsync();