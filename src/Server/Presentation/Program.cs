using FinanceLab.Server.Application.Extensions;
using FinanceLab.Server.Infrastructure.Converters;
using FinanceLab.Server.Infrastructure.Extensions;
using FinanceLab.Server.Persistence.Extensions;
using FinanceLab.Server.Presentation.Resources;
using FinanceLab.Shared.Application.Abstractions;
using Hellang.Middleware.ProblemDetails;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddLocalization().AddTransient<ISharedResources, SharedResources>();
builder.Services.AddMongoDb(connectionString);
builder.Services.AddMappers();
builder.Services.AddHandlers();
builder.Services.AddCookieAuthentication();
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews()
    .AddDataAnnotationsLocalization()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new DateTimeOffsetConverter()));
builder.Services.AddValidators();
builder.Services.AddProblemDetailsWithConventions();
builder.Services.AddBinanceService();

var app = builder.Build();
var supportedCultures = new[] { "en-US", "tr-TR" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseProblemDetails();
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseRequestLocalization(localizationOptions);

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();