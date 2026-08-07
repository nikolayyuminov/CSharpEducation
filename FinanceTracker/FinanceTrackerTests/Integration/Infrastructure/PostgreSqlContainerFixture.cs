using DotNet.Testcontainers.Images;
using Testcontainers.PostgreSql;

namespace FinanceTrackerTests.Integration.Infrastructure;

/// <summary>
/// Контейнер PostgreSQL для интеграционных тестов.
/// </summary>
public sealed class PostgreSqlContainerFixture
{
  private readonly PostgreSqlContainer _container =
    new PostgreSqlBuilder()
      .WithImage("postgres:16")
      .WithDatabase("FinanceTrackerTests")
      .WithImagePullPolicy(PullPolicy.Never)
      .WithUsername("postgres")
      .WithPassword("postgres")
      .Build();

  /// <summary>
  /// Строка подключения.
  /// </summary>
  public string ConnectionString => _container.GetConnectionString();

  /// <summary>
  /// Запустить контейнер.
  /// </summary>
  public Task StartAsync()
  {
    return _container.StartAsync();
  }

  /// <summary>
  /// Остановить контейнер.
  /// </summary>
  public Task DisposeAsync()
  {
    return _container.DisposeAsync().AsTask();
  }
}