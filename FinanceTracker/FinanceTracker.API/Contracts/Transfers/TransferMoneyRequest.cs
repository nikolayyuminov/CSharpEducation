namespace FinanceTracker.API.Contracts.Transfers;

/// <summary>
/// Запрос от пользователя на перевод средств между счетами.
/// </summary>
public class TransferMoneyRequest
{
  /// <summary>
  /// Откуда списать средства.
  /// </summary>
  public long FromAccountId { get; init; }

  /// <summary>
  /// Куда зачислить средства.
  /// </summary>
  public long ToAccountId { get; init; }

  /// <summary>
  /// Сумма перевода между счетами.
  /// </summary>
  public decimal Amount { get; init; }
  
  /// <summary>
  /// Описание перевода между счетами.
  /// </summary>
  public string? Description { get; init; }
}