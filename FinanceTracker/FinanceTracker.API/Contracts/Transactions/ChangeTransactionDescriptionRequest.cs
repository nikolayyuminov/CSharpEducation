namespace FinanceTracker.API.Contracts.Transactions;

/// <summary>
/// Запрос от пользователя на изменение описания транзакции.
/// </summary>
public class ChangeTransactionDescriptionRequest
{
  /// <summary>
  /// Id транзакции для изменения описания.
  /// </summary>
  public long TransactionId { get; init; }
  
  /// <summary>
  /// Новое описание для транзакции.
  /// </summary>
  public string? NewDescription { get; init; }
}