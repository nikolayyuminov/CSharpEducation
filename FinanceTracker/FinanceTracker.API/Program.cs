using FinanceTracker.Application.DependencyInjection;
using FinanceTracker.Infrastructure.DependencyInjection;

namespace FinanceTracker.API;

/// <summary>
/// Точка входа в приложение.
/// </summary>
public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    builder.Services.AddOpenApi();
    
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
      app.MapOpenApi();
    }
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
    
  }
}