using FinanceTracker.Application.Abstractions;

namespace FinanceTracker.Infrastructure.Persistence;

/// <summary>
/// Юнит для работы с БД.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
  #region Поля и свойства

  /// <summary>
  /// Контекст БД.
  /// </summary>
  private readonly FinanceTrackerDbContext _dbContext;

  #endregion

  #region Методы

  /// <summary>
  /// Сохранить изменения.
  /// </summary>
  public void SaveChanges()
  {
    _dbContext.SaveChanges();
  }

  #endregion

  #region Конструкторы

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="dbContext">Контекст БД.</param>
  public UnitOfWork(FinanceTrackerDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  #endregion
}