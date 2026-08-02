using FinanceTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Infrastructure.Persistence;

/// <summary>
/// Контекст базы данных приложения FinanceTracker.
/// Содержит наборы сущностей и конфигурацию модели Entity Framework Core.
/// </summary>
public class FinanceTrackerDbContext : DbContext
{
  #region Поля и свойства
  
  /// <summary>
  /// Таблица счетов.
  /// </summary>
  public DbSet<Account> Accounts => Set<Account>();
  
  /// <summary>
  /// Таблица транзакций.
  /// </summary>
  public DbSet<Transaction> Transactions => Set<Transaction>();
  
  /// <summary>
  /// Таблица категорий.
  /// </summary>
  public DbSet<Category> Categories => Set<Category>();

  #endregion

  #region Методы

  /// <summary>
  /// Настроить модель базы данных.
  /// Загружает все конфигурации сущностей из текущей сборки.
  /// </summary>
  /// <param name="modelBuilder">Построитель модели Entity Framework Core.</param>
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinanceTrackerDbContext).Assembly);
    base.OnModelCreating(modelBuilder);
  }

  #endregion
  
  #region Конструктор

  /// <summary>
  /// Конструктор.
  /// Инициализирует новый экземпляр контекста базы данных.
  /// </summary>
  /// <param name="options">Параметры конфигурации контекста базы данных.</param>
  public FinanceTrackerDbContext(DbContextOptions<FinanceTrackerDbContext> options) : base(options) { }

  #endregion
}