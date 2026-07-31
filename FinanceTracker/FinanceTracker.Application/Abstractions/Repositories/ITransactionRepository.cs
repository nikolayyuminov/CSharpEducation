using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория транзакций.
/// </summary>
public interface ITransactionRepository
{
  /// <summary>
  /// Добавить транзакцию в репозиторий.
  /// </summary>
  /// <param name="transaction">Транзакция.</param>
  void Add(Transaction transaction);

  /// <summary>
  /// Получить транзакцию по Id
  /// </summary>
  /// <param name="transactionId">Id транзакции.</param>
  /// <returns>Транзакция. Может быть пустой.</returns>
  Transaction? GetById(long transactionId);
}