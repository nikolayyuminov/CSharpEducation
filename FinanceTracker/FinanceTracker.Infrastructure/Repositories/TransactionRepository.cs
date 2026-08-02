using FinanceTracker.Application.Abstractions.Repositories;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Infrastructure.Persistence;

namespace FinanceTracker.Infrastructure.Repositories;

/// <summary>
/// Репозиторий транзакций.
/// </summary>
public class TransactionRepository : ITransactionRepository
{
  #region Поля и свойства

  /// <summary>
  /// Контекст БД.
  /// </summary>
  private readonly FinanceTrackerDbContext _dbContext;

  #endregion
  
  #region Методы

  /// <summary>
  /// Добавить транзакцию в репозиторий.
  /// </summary>
  /// <param name="transaction">Транзакция.</param>
  public void Add(Transaction transaction)
  {
    _dbContext.Transactions.Add(transaction);
  }

  /// <summary>
  /// Получить транзакцию по Id.
  /// </summary>
  /// <param name="transactionId">Id транзакции.</param>
  /// <returns>Транзакция. Может быть пустой.</returns>
  public Transaction? GetById(long transactionId)
  {
    return _dbContext.Transactions.Find(transactionId);
  }

  #endregion

  #region Конструкторы

  public TransactionRepository(FinanceTrackerDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  #endregion
}