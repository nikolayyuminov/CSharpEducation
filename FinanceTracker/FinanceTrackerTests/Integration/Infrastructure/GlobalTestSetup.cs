using FinanceTracker.Infrastructure.Persistence;
using FinanceTrackerTests.Integration.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace FinanceTrackerTests;

/// <summary>
/// Глобальная настройка интеграционных тестов.
/// Выполняется один раз перед запуском всех тестов.
/// </summary>
[SetUpFixture]
public sealed class GlobalTestSetup
{
  public static DatabaseReset DatabaseReset { get; private set; } = null!;
  
  public static PostgreSqlContainerFixture Database { get; private set; } = null!;

  public static TestWebApplicationFactory Factory { get; private set; } = null!;

  [OneTimeSetUp]
  public async Task Setup()
  {
    Database = new PostgreSqlContainerFixture();

    await Database.StartAsync();

    Factory = new TestWebApplicationFactory(Database);

    using var client = Factory.CreateClient();

    using var scope = Factory.Services.CreateScope();

    var db = scope.ServiceProvider.GetRequiredService<FinanceTrackerDbContext>();

    await db.Database.MigrateAsync();

    DatabaseReset = new DatabaseReset(Database.ConnectionString);

    await DatabaseReset.InitializeAsync();
  }

  [OneTimeTearDown]
  public async Task TearDown()
  {
    Factory.Dispose();

    await Database.DisposeAsync();
  }
}