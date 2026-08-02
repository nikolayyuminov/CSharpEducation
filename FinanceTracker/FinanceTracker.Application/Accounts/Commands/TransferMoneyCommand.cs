namespace FinanceTracker.Application.Accounts.Commands;

/// <summary>
/// Команда перевода средств между счетами.
/// </summary>
public class TransferMoneyCommand
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