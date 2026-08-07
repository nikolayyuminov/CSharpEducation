using Npgsql;
using Respawn;
using Respawn.Graph;

namespace FinanceTrackerTests.Integration.Infrastructure;

/// <summary>
/// Сбрасывает состояние базы данных между тестами.
/// </summary>
public sealed class DatabaseReset
{
  private readonly string _connectionString;

  private Respawner _respawner = null!;

  public DatabaseReset(string connectionString)
  {
    _connectionString = connectionString;
  }

  public async Task InitializeAsync()
  {
    await using var connection = new NpgsqlConnection(_connectionString);

    await connection.OpenAsync();

    _respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
    {
      DbAdapter = DbAdapter.Postgres,

      SchemasToInclude = new[]
      {
        "public"
      },

      TablesToIgnore = new[]
      {
        new Table("__EFMigrationsHistory")
      }
    });
  }

  public async Task ResetAsync()
  {
    await using var connection = new NpgsqlConnection(_connectionString);

    await connection.OpenAsync();

    await _respawner.ResetAsync(connection);
  }
}