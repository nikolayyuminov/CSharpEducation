using FinanceTracker.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FinanceTracker.API;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FinanceTrackerTests.Integration.Infrastructure;

/// <summary>
/// Фабрика тестового ASP.NET Core приложения.
/// </summary>
public sealed class TestWebApplicationFactory : WebApplicationFactory<Program>
{
  private readonly PostgreSqlContainerFixture _database;

  public TestWebApplicationFactory(PostgreSqlContainerFixture database)
  {
    _database = database;
  }

  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureServices(services =>
    {
      var descriptor = services
        .SingleOrDefault(
          d => d.ServiceType == typeof(DbContextOptions<FinanceTrackerDbContext>));

      if (descriptor != null)
      {
        services.Remove(descriptor);
      }


      services.AddDbContext<FinanceTrackerDbContext>(options =>
      {
        options.UseNpgsql(_database.ConnectionString);
      });
    });
  }
}