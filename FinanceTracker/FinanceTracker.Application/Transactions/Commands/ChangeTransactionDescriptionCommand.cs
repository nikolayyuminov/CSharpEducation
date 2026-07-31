namespace FinanceTracker.Application.Transactions.Commands;

/// <summary>
/// Команда изменения описания транзакции.
/// </summary>
public class ChangeTransactionDescriptionCommand
{
  #region Поля и свойства
  
  /// <summary>
  /// Id транзакции для изменения описания.
  /// </summary>
  public long TransactionId { get; init; }
  
  /// <summary>
  /// Новое описание для транзакции.
  /// </summary>
  public string? NewDescription { get; init; }

  #endregion
}