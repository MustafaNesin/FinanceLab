using FinanceLab.Server.Application.Extensions;
using FinanceLab.Server.Infrastructure.Extensions;
using FinanceLab.Server.Persistence.Extensions;
using Hellang.Middleware.ProblemDetails;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddMongoDb(connectionString);
builder.Services.AddMappers();
builder.Services.AddHandlers();
builder.Services.AddCookieAuthentication();
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddValidators();
builder.Services.AddProblemDetailsWithConventions();

var app = builder.Build();

app.UseProblemDetails();
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();