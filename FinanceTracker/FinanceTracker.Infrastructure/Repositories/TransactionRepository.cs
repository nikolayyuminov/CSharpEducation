using FinanceTracker.Application.Abstractions.Repositories;
using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Infrastructure.Repositories;

/// <summary>
/// Репозиторий транзакций.
/// </summary>
public class TransactionRepository : ITransactionRepository
{
  #region Поля и свойства

  /// <summary>
  /// Коллекция транзакций.
  /// </summary>
  private  readonly List<Transaction> _transactions;

  #endregion
  
  #region Методы

  /// <summary>
  /// Добавить транзакцию в репозиторий.
  /// </summary>
  /// <param name="transaction">Транзакция.</param>
  public void Add(Transaction transaction)
  {
    _transactions.Add(transaction);
  }

  /// <summary>
  /// Получить транзакцию по Id.
  /// </summary>
  /// <param name="transactionId">Id транзакции.</param>
  /// <returns>Транзакция. Может быть пустой.</returns>
  public Transaction? GetById(long transactionId)
  {
    return _transactions.FirstOrDefault(t => t.Id == transactionId);
  }

  #endregion

  #region Конструкторы

  public TransactionRepository()
  {
    _transactions = [];
  }

  #endregion
}