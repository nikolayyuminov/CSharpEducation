using System.Net.Http;
using FinanceTracker.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace FinanceTrackerTests.Integration.Infrastructure;

/// <summary>
/// Базовый класс интеграционных тестов.
/// </summary>
public abstract class IntegrationTestBase
{
  protected HttpClient Client = null!;

  protected TestWebApplicationFactory Factory => GlobalTestSetup.Factory;

  [SetUp]
  public void SetUp()
  {
    Client = Factory.CreateClient();
  }

  protected async Task ExecuteDbContextAsync(
    Func<FinanceTrackerDbContext, Task> action)
  {
    using var scope = Factory.Services.CreateScope();

    var db = scope.ServiceProvider
      .GetRequiredService<FinanceTrackerDbContext>();

    await action(db);
  }
  
  [TearDown]
  public async Task TearDown()
  {
    Client.Dispose();

    await GlobalTestSetup.DatabaseReset.ResetAsync();
  }
}