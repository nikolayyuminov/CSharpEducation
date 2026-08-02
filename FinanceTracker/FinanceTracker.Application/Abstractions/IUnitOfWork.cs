namespace FinanceTracker.Application.Abstractions;

/// <summary>
/// Интерфейс юнита для работы с БД.
/// </summary>
public interface IUnitOfWork
{
  /// <summary>
  /// Сохранить изменения в БД.
  /// </summary>
  void SaveChanges();
}